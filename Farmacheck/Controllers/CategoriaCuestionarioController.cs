using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;

namespace Farmacheck.Controllers
{
    public class CategoriaCuestionarioController : Controller
    {
        private readonly ICategoryByQuestionnaireApiClient _apiClient;
        private readonly IMapper _mapper;

        public CategoriaCuestionarioController(ICategoryByQuestionnaireApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var apiData = await _apiClient.GetCategoriesAsync();

                var dtos = _mapper.Map<List<CategoryByQuestionnaireDto>>(apiData); // <- puede fallar
                var categorias = _mapper.Map<List<CategoriaCuestionarioViewModel>>(dtos);

                return View(categorias);
            }
            catch (Exception ex)
            {
                return Content($"Error en Index: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetCategoriesAsync();
            var dtos = _mapper.Map<List<CategoryByQuestionnaireDto>>(apiData);
            var categorias = _mapper.Map<List<CategoriaCuestionarioViewModel>>(dtos);

            return Json(new { success = true, data = categorias });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(byte id)
        {
            var entidad = await _apiClient.GetCategoryAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<CategoryByQuestionnaireDto>(entidad);
            var model = _mapper.Map<CategoriaCuestionarioViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] CategoriaCuestionarioViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre))
                    return Json(new { success = false, error = "El nombre es obligatorio." });

                var request = _mapper.Map<CategoryByQuestionnaireRequest>(model);

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updateRequest = _mapper.Map<UpdateCategoryByQuestionnaireRequest>(model);
                    var updated = await _apiClient.UpdateAsync(updateRequest);
                    if (updated)
                        return Json(new { success = true, id = model.Id });

                    return Json(new { success = false, error = "No se pudo actualizar" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(byte id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
