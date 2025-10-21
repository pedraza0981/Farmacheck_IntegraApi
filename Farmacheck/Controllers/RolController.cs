using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Roles;
using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.PermissionsByRoles;
using Farmacheck.Application.Models.Permissions;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.Menus;
using Farmacheck.Models.Request;
using System;
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
        private readonly IMenuApiClient _menuApiClient;
        private readonly IRolMenuApiClient _rolMenuApiClient;
        private readonly IMapper _mapper;

        private static readonly Dictionary<int, List<int>> _permisosPorRol = new();

        public RolController(IRoleApiClient apiClient,
                             IBusinessUnitApiClient businessUnitApi,
                             IPermissionApiClient permissionApi,
                             IPermissionByRoleApiClient permissionByRoleApi,
                             IMenuApiClient menuApiClient,
                             IRolMenuApiClient rolMenuApiClient,
                             IMapper mapper)
        {
            _apiClient = apiClient;
            _businessUnitApi = businessUnitApi;
            _permissionApi = permissionApi;
            _permissionByRoleApi = permissionByRoleApi;
            _menuApiClient = menuApiClient;
            _rolMenuApiClient = rolMenuApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int unidadId)
        {
            ViewBag.UnidadId = unidadId;

            var apiData = await _apiClient.GetAllRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = _mapper.Map<List<RolViewModel>>(dtos)
                .OrderBy(r => r.Nombre ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var unidadesApi = await _businessUnitApi.GetBusinessUnitsAsync();
            foreach (var r in roles)
            {
                var u = unidadesApi.FirstOrDefault(b => b.Id == r.UnidadDeNegocioId);
                r.UnidadDeNegocioNombre = u?.Nombre;
            }

            var menus = await _menuApiClient.GetMenusAsync();
            var menuTree = BuildTree(menus?.ToList() ?? new List<MenuResponse>());
            ViewBag.MenuTree = menuTree;

            return View(roles);
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int unidadId)
        {
            var apiData = await _apiClient.GetAllRolesAsync();
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = _mapper.Map<List<RolViewModel>>(dtos)
                .OrderBy(r => r.Nombre ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var unidadesApi = await _businessUnitApi.GetBusinessUnitsAsync();
            foreach (var r in roles)
            {
                var u = unidadesApi.FirstOrDefault(b => b.Id == r.UnidadDeNegocioId);
                r.UnidadDeNegocioNombre = u?.Nombre;
            }

            return Json(new { success = true, data = roles });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPorUnidadNegocio(int unidadId)
        {
            var apiData = await _apiClient.GetRolesByBusinessUnitAsync((byte)unidadId);
            var dtos = _mapper.Map<List<RoleDto>>(apiData);
            var roles = _mapper.Map<List<RolViewModel>>(dtos)
                .OrderBy(r => r.Nombre ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();

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
                var existente = await _apiClient.GetRoleByNameAsync(model.Nombre);
                if (existente.Id != 0)
                {
                    return Json(new { success = false, error = "El rol ya ha sido registrado." });
                }

                var request = _mapper.Map<RoleRequest>(model);
                var id = await _apiClient.CreateAsync(request);
                _permisosPorRol[id] = model.Permisos ?? new List<int>();

                if (model.Permisos != null && model.Permisos.Any())
                {
                    var permisos = await _permissionApi.GetPermissionsAsync();
                    var seleccionados = permisos
                        .Where(p => model.Permisos.Contains(p.Id))
                        .Select(p => p.Id) 
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

                        var nuevosIds2 = idsActuales.Except(idsAsignadosOriginal).ToList();

                        var idsEliminados = idsAsignadosOriginal.Except(idsActuales).ToList();

                        var seleccionados = permisos
                            .Where(p => nuevosIds2.Contains(p.Id))
                            .Select(p => p.Id) 
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

                    return Json(new { success = true, message = "Actualización realizada correctamente", id = model.Id });
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

        [HttpGet]
        public async Task<IActionResult> DescargarReporte()
        {
            var base64 = await _apiClient.GetReport();
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteRoles.xlsx");
        }

#region MenuAssignment

        private static List<MenuTreeNode> BuildTree(List<MenuResponse> menus, int? parentId = null)
        {
            return menus
                .Where(menu => menu.ParentId == parentId)
                .OrderBy(menu => menu.Orden)
                .ThenBy(menu => menu.Nombre)
                .Select(menu => new MenuTreeNode
                {
                    Id = menu.Id,
                    Nombre = menu.Nombre,
                    Hijos = BuildTree(menus, menu.Id)
                })
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> GetMenusByRole(int roleId)
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusByRolAsync(roleId);

            if (rolMenus == null)
            {
                return Json(Enumerable.Empty<object>());
            }

            return Json(rolMenus);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarAsignaciones([FromBody] SaveMenuRoleAssignmentRequest request)
        {
            if (request == null || request.RoleId <= 0)
            {
                return Json(new { success = false, message = "Debe seleccionar un rol válido." });
            }

            try
            {
                var existingRolMenus = await _rolMenuApiClient.GetRolMenusByRolAsync(request.RoleId);
                var existingMenuIds = existingRolMenus?
                    .Where(menu => menu != null && menu.PuedeVer)
                    .Select(menu => menu.MenuId)
                    .ToHashSet() ?? new HashSet<int>();

                var desiredMenuIds = (request.MenuIds ?? new List<int>())
                    .Where(id => id > 0)
                    .ToHashSet();

                var menuIdsToAdd = desiredMenuIds.Except(existingMenuIds).ToList();
                var menuIdsToRemove = existingMenuIds.Except(desiredMenuIds).ToList();

                var creationTasks = menuIdsToAdd
                    .Select(menuId => _rolMenuApiClient.CreateRolMenuAsync(new RolMenuRequest
                    {
                        RolId = request.RoleId,
                        MenuId = menuId,
                        PuedeVer = true
                    }));

                var deletionTasks = menuIdsToRemove
                    .Select(menuId => _rolMenuApiClient.DeleteRolMenuAsync(request.RoleId, menuId));

                await Task.WhenAll(creationTasks);
                await Task.WhenAll(deletionTasks);

                var message = "Asignación actualizada correctamente.";
                if (menuIdsToAdd.Any() || menuIdsToRemove.Any())
                {
                    message = $"Asignación actualizada. Menús agregados: {menuIdsToAdd.Count}. Menús eliminados: {menuIdsToRemove.Count}.";
                }

                return Json(new
                {
                    success = true,
                    message,
                    added = menuIdsToAdd,
                    removed = menuIdsToRemove
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "No se pudo guardar la asignación de menús.",
                    error = ex.Message
                });
            }
        }

#endregion
    }
}
