using AutoMapper;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Categories;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IMapper _mapper;

        public CategoriaController(
            ICategoryApiClient categoryApiClient,
            IRoleApiClient roleApiClient,
            IMapper mapper)
        {
            _categoryApiClient = categoryApiClient;
            _roleApiClient = roleApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = (await _categoryApiClient.GetAllCategoriesAsync()).ToList();
            var roles = await _roleApiClient.GetAllRolesAsync();

            var viewModels = _mapper.Map<List<CategoriaViewModel>>(categories);

            var rolesDictionary = roles.ToDictionary(r => r.Id, r => r.Nombre);
            foreach (var category in viewModels)
            {
                if (rolesDictionary.TryGetValue(category.RolId, out var roleName))
                {
                    category.RolNombre = roleName;
                }
            }

            return View(viewModels);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var categories = (await _categoryApiClient.GetAllCategoriesAsync()).ToList();
            var roles = await _roleApiClient.GetAllRolesAsync();

            var items = _mapper.Map<List<CategoriaViewModel>>(categories);

            var rolesDictionary = roles.ToDictionary(r => r.Id, r => r.Nombre);
            foreach (var category in items)
            {
                if (rolesDictionary.TryGetValue(category.RolId, out var roleName))
                {
                    category.RolNombre = roleName;
                }
            }

            return Json(new { success = true, data = items });
        }

        [HttpGet]
        public async Task<JsonResult> ListarRoles()
        {
            var roles = await _roleApiClient.GetAllRolesAsync();
            var data = roles.Select(r => new { id = r.Id, nombre = r.Nombre, estatus = r.Estatus });
            return Json(new { success = true, data });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var category = await _categoryApiClient.GetCategoryAsync(id);
            if (category == null)
            {
                return Json(new { success = false, error = "No encontrado" });
            }

            var model = _mapper.Map<CategoriaViewModel>(category);

            var roles = await _roleApiClient.GetAllRolesAsync();
            model.RolNombre = roles.FirstOrDefault(r => r.Id == model.RolId)?.Nombre;

            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] CategoriaViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                return Json(new { success = false, error = "El nombre es obligatorio." });
            }

            if (model.RolId <= 0)
            {
                return Json(new { success = false, error = "El rol es obligatorio." });
            }

            if (model.Id == 0)
            {
                var request = _mapper.Map<CategoryRequest>(model);
                var id = await _categoryApiClient.CreateAsync(request);
                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateCategoryRequest>(model);
                updateRequest.Estatus = model.Estatus;
                var updated = await _categoryApiClient.UpdateAsync(updateRequest);
                if (updated)
                {
                    return Json(new { success = true, id = model.Id });
                }

                return Json(new { success = false, error = "No se pudo actualizar" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            var deleted = await _categoryApiClient.DeleteAsync(id);
            return Json(new { success = deleted });
        }

        [HttpGet]
        public async Task<IActionResult> DescargarReporte()
        {
            var base64 = await _categoryApiClient.GetReportAsync();
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteCategorias.xlsx");
        }
    }
}
