using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Zones;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Controllers
{
    public class ZonaController : Controller
    {
        private readonly IZoneApiClient _apiClient;
        private readonly IMapper _mapper;

        public ZonaController(IZoneApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetAllZonesAsync();

            // Map first to DTOs and then to the ViewModel to avoid missing configuration
            var dtos = _mapper.Map<List<ZonaDto>>(apiData);
            var items = _mapper.Map<List<ZonaViewModel>>(dtos);

            return View(items);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetAllZonesAsync();

            // Map first to DTOs and then to the ViewModel to avoid missing configuration
            var dtos = _mapper.Map<List<ZonaDto>>(apiData);
            var items = _mapper.Map<List<ZonaViewModel>>(dtos);

            return Json(new { success = true, data = items });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetZoneAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<ZonaDto>(entidad);
            var model = _mapper.Map<ZonaViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] ZonaViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre))
                    return Json(new { success = false, error = "El nombre es obligatorio." });

                model.Estatus ??= true;

                var request = _mapper.Map<ZoneRequest>(model);

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updateRequest = _mapper.Map<UpdateZoneRequest>(model);
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
