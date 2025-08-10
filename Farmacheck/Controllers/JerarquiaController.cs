using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.HierarchyByRoles;
using Farmacheck.Application.Models.Roles;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Controllers
{
    public class JerarquiaController : Controller
    {
        private readonly IHierarchyByRoleApiClient _apiClient;
        private readonly IRoleApiClient _roleApi;
        private readonly IMapper _mapper;
        private const int _itemsPerPage = 5;

        public JerarquiaController(IHierarchyByRoleApiClient apiClient,
                                   IRoleApiClient roleApi,
                                   IMapper mapper)
        {
            _apiClient = apiClient;
            _roleApi = roleApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await ObtenerJerarquiasAsync(page, _itemsPerPage);
            ViewBag.Page = page;
            ViewBag.HasMore = result.HasNextPage;
            return View(result.Items.ToList());
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int page = 1)
        {
            var result = await ObtenerJerarquiasAsync(page, _itemsPerPage);
            return Json(new { success = true, data = result });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<HierarchyByRoleDto>(entidad);
            var model = _mapper.Map<JerarquiaViewModel>(dto);
            await CompletarNombresRoles(new List<JerarquiaViewModel> { model });

            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] List<JerarquiaViewModel> modelos)
        {
            try
            {
                if (modelos == null || modelos.Count == 0)
                    return Json(new { success = false, error = "Sin datos" });

                //var existentes = await _apiClient.GetAllAsync();
                foreach (var m in modelos)
                {
                    //if (existentes.Any(e => e.RolSuperiorId == m.RolSuperiorId && e.RolSubordinadoId == m.RolSubordinadoId))
                    //    continue;

                    var request = _mapper.Map<HierarchyByRoleRequest>(m);
                    await _apiClient.CreateAsync(request);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GuardarUno([FromBody] JerarquiaViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre))
                    return Json(new { success = false, error = "El nombre es obligatorio." });

                if (model.Id == 0)
                {
                    var request = _mapper.Map<HierarchyByRoleRequest>(model);
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var update = _mapper.Map<UpdateHierarchyByRoleRequest>(model);
                    var updated = await _apiClient.UpdateAsync(update);
                    return Json(new { success = updated });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }

        private async Task<PaginatedResponse<JerarquiaViewModel>> ObtenerJerarquiasAsync(int page, int items)
        {
            var apiData = await _apiClient.GetByPageAsync(page, items);
            var dtos = _mapper.Map<List<HierarchyByRoleDto>>(apiData.Items);
            var models = _mapper.Map<List<JerarquiaViewModel>>(dtos);

            await CompletarNombresRoles(models);

            return new PaginatedResponse<JerarquiaViewModel>();
        }

        private async Task CompletarNombresRoles(IEnumerable<JerarquiaViewModel> modelos)
        {
            var roles = await _roleApi.GetRolesAsync();
            foreach (var m in modelos)
            {
                var sup = roles.FirstOrDefault(r => r.Id == m.RolSuperiorId);
                var sub = roles.FirstOrDefault(r => r.Id == m.RolSubordinadoId);
                m.RolSuperiorNombre = sup?.Nombre;
                m.RolSubordinadoNombre = sub?.Nombre;
            }
        }
    }
}
