using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Roles;
using AutoMapper;
using Farmacheck.Application.DTOs;
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
        private readonly IMapper _mapper;

        private static readonly Dictionary<int, List<int>> _permisosPorRol = new();

        public RolController(IRoleApiClient apiClient,
                             IBusinessUnitApiClient businessUnitApi,
                             IPermissionApiClient permissionApi,
                             IMapper mapper)
        {
            _apiClient = apiClient;
            _businessUnitApi = businessUnitApi;
            _permissionApi = permissionApi;
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
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetRoleAsync((byte)id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<RoleDto>(entidad);
            var model = _mapper.Map<RolViewModel>(dto);

            var unidad = await _businessUnitApi.GetBusinessUnitAsync(model.UnidadDeNegocioId);
            model.UnidadDeNegocioNombre = unidad?.Nombre;

            return Json(new { success = true, data = model });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPermisos(int id)
        {
            _permisosPorRol.TryGetValue(id, out var asignados);
            asignados ??= new List<int>();

            var permisos = await _permissionApi.GetPermissionsAsync();
            var data = permisos.Select(p => new
            {
                p.Id,
                p.Nombre,
                Asignado = asignados.Contains(p.Id)
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
                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateRoleRequest>(model);
                var updated = await _apiClient.UpdateAsync(updateRequest);
                if (updated)
                {
                    _permisosPorRol[model.Id] = model.Permisos ?? new List<int>();
                    return Json(new { success = true, id = model.Id });
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
