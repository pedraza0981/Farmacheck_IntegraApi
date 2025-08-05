using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Roles;
using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.PermissionsByRoles;
using Farmacheck.Application.Models.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class RolController : Controller
    {
        private readonly IRoleApiClient _apiClient;
        private readonly IBusinessUnitApiClient _businessUnitApi;
        private readonly IPermissionApiClient _permissionApi;
        private readonly IPermissionByRoleApiClient _permissionByRoleApi;
        private readonly IMapper _mapper;

        private static readonly Dictionary<int, List<int>> _permisosPorRol = new();

        public RolController(IRoleApiClient apiClient,
                             IBusinessUnitApiClient businessUnitApi,
                             IPermissionApiClient permissionApi,
                             IPermissionByRoleApiClient permissionByRoleApi,
                             IMapper mapper)
        {
            _apiClient = apiClient;
            _businessUnitApi = businessUnitApi;
            _permissionApi = permissionApi;
            _permissionByRoleApi = permissionByRoleApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int unidadId)
        {
            ViewBag.UnidadId = unidadId;

            var apiData = await _apiClient.GetRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = _mapper.Map<List<RolViewModel>>(dtos);

            if (unidadId > 0)
                roles = roles.Where(r => r.UnidadDeNegocioId == unidadId).ToList();

            var unidadesApi = await _businessUnitApi.GetBusinessUnitsAsync();
            foreach (var r in roles)
            {
                var u = unidadesApi.FirstOrDefault(b => b.Id == r.UnidadDeNegocioId);
                r.UnidadDeNegocioNombre = u?.Nombre;
            }

            return View(roles);
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int unidadId)
        {
            var apiData = await _apiClient.GetRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = _mapper.Map<List<RolViewModel>>(dtos);

            if (unidadId > 0)
                roles = roles.Where(r => r.UnidadDeNegocioId == unidadId).ToList();

            var unidadesApi = await _businessUnitApi.GetBusinessUnitsAsync();
            foreach (var r in roles)
            {
                var u = unidadesApi.FirstOrDefault(b => b.Id == r.UnidadDeNegocioId);
                r.UnidadDeNegocioNombre = u?.Nombre;
            }

            return Json(new { success = true, data = roles });
        }

        [HttpGet]
        public async Task<JsonResult> ListarGestion()
        {
            var apiData = await _apiClient.GetRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = dtos.Select(r => new { id = r.Id, nombre = r.Nombre });
            return Json(new { success = true, data = roles });
        }

        [HttpGet]
        public async Task<JsonResult> ListarUnidades()
        {
            var unidades = await _businessUnitApi.GetBusinessUnitsAsync();
            var data = unidades.Select(u => new { id = u.Id, nombre = u.Nombre });
            return Json(new { success = true, data });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetRoleAsync((byte)id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<RoleDto>(entidad);
            var model = _mapper.Map<RolViewModel>(dto);

            var unidad = await _businessUnitApi.GetBusinessUnitAsync(model.UnidadDeNegocioId);
            model.UnidadDeNegocioNombre = unidad?.Nombre;

            var permisosAsignados = await _permissionByRoleApi.GetByRolAsync(id);
            
            model.Permisos = permisosAsignados?.Select(p => p.PermisoId).ToList() ?? new List<int>();
            _permisosPorRol[id] = model.Permisos;

            return Json(new { success = true, data = model });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPermisos(int id)
        {
            var permisos = await _permissionApi.GetPermissionsAsync();
            var data = permisos.Select(p => new
            {
                p.Id,
                p.Nombre
            });

            return Json(new { success = true, data });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] RolViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.Id == 0)
            {
                var request = _mapper.Map<RoleRequest>(model);
                var id = await _apiClient.CreateAsync(request);
                _permisosPorRol[id] = model.Permisos ?? new List<int>();

                if (model.Permisos != null && model.Permisos.Any())
                {
                    var permisos = await _permissionApi.GetPermissionsAsync();
                    var seleccionados = permisos
                        .Where(p => model.Permisos.Contains(p.Id))
                        .ToList();

                    var permisoRolRequest = new PermissionByRoleRequest
                    {
                        RolId = id,
                        Permisos = seleccionados
                    };

                    await _permissionByRoleApi.CreateAsync(permisoRolRequest);
                }

                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateRoleRequest>(model);
                var updated = await _apiClient.UpdateAsync(updateRequest);
                if (updated)
                {
                    var permisosAsignados = ViewBag.PermisosAsignados as List<PermissionByRoleResponse>
                                            ?? await _permissionByRoleApi.GetByRolAsync(model.Id);

                    var asignadosIds = permisosAsignados.Select(p => p.PermisoId).ToList();
                    var seleccionadosIds = model.Permisos ?? new List<int>();

                    var nuevosIds = seleccionadosIds.Except(asignadosIds).ToList();
                    var removidosIds = asignadosIds.Except(seleccionadosIds).ToList();

                    if (nuevosIds.Any())
                    {
                        var permisos = await _permissionApi.GetPermissionsAsync();
                        var permisosAsignadosOriginal = await _permissionByRoleApi.GetByRolAsync(model.Id);

                        var idsAsignadosOriginal = permisosAsignadosOriginal.Select(p => p.PermisoId).ToList();
                        var idsActuales = model.Permisos;

                        // ? Detectar nuevos permisos a insertar
                        var nuevosIds = idsActuales.Except(idsAsignadosOriginal).ToList();

                        // ? Detectar permisos eliminados
                        var idsEliminados = idsAsignadosOriginal.Except(idsActuales).ToList();

                        // Obtener los objetos Permission completos (para insertar los nuevos)
                        var seleccionados = permisos
                            .Where(p => nuevosIds.Contains(p.Id))
                            .ToList();

                        if (seleccionados.Any())
                        {
                            var permisoRolRequest = new PermissionByRoleRequest
                            {
                                RolId = model.Id,
                                Permisos = seleccionados
                            };

                            await _permissionByRoleApi.CreateAsync(permisoRolRequest);
                        }

                        // Eliminar permisos que ya no est·n
                        if (idsEliminados.Any())
                        {
                            foreach (var permisoId in idsEliminados)
                            {
                                await _permissionByRoleApi.DeleteAsync(permisoId);
                            }
                        }
                    }

                    foreach (var idPermiso in removidosIds)
                    {
                        await _permissionApi.DeleteAsync(idPermiso);
                    }

                    _permisosPorRol[model.Id] = seleccionadosIds;

                    return Json(new { success = true, message = "Actualizaci√≥n realizada correctamente", id = model.Id });
                }

                return Json(new { success = false, error = "No se pudo actualizar" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync((byte)id);
            //_permisosPorRol.Remove(id);
            return Json(new { success = true });
        }
    }
}
