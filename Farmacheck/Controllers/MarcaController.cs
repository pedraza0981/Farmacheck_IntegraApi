using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.Brands;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;

namespace Farmacheck.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IBrandApiClient _apiClient;
        private readonly IMapper _mapper;

        public MarcaController(IBrandApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int unidadId)
        {
            ViewBag.UnidadId = unidadId;

            var apiData = await _apiClient.GetBrandsAsync();
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var marcas = _mapper.Map<List<MarcaViewModel>>(dtos);

            return View(marcas);
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int unidadId)
        {
            var apiData = await _apiClient.GetBrandsAsync();
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var marcas = _mapper.Map<List<MarcaViewModel>>(dtos);

            return Json(new { success = true, data = marcas });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetBrandAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<MarcaDto>(entidad);
            var model = _mapper.Map<MarcaViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromForm] MarcaViewModel model, IFormFile? LogotipoArchivo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre))
                    return Json(new { success = false, error = "El nombre es obligatorio." });

                if (LogotipoArchivo != null && LogotipoArchivo.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await LogotipoArchivo.CopyToAsync(ms);
                    model.Logotipo = Convert.ToBase64String(ms.ToArray());
                    model.LogotipoNombreArchivo = LogotipoArchivo.FileName;
                }

                var request = _mapper.Map<BrandRequest>(model);

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updateRequest = _mapper.Map<UpdateBrandRequest>(model);
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
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
