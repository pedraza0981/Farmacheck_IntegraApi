using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.BusinessUnits;

namespace Farmacheck.Controllers
{
    public class UnidadDeNegocioController : Controller
    {
        private readonly IBusinessUnitApiClient _apiClient;
        private readonly IMapper _mapper;

        public UnidadDeNegocioController(IBusinessUnitApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetBusinessUnitsAsync();
            var dtos = _mapper.Map<List<BusinessUnitDto>>(apiData);
            var unidades = _mapper.Map<List<UnidadDeNegocio>>(dtos);
            return View(unidades);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetBusinessUnitsAsync();
            var dtos = _mapper.Map<List<BusinessUnitDto>>(apiData);
            var unidades = _mapper.Map<List<UnidadDeNegocio>>(dtos);
            return Json(new { success = true, data = unidades });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetBusinessUnitAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<BusinessUnitDto>(entidad);
            var model = _mapper.Map<UnidadDeNegocio>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromForm] UnidadDeNegocio model, IFormFile? LogotipoArchivo)
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
                    model.Estatus = true;


                }

                var request = _mapper.Map<BusinessUnitRequest>(model);

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updated = await _apiClient.UpdateAsync(request);
                    if (updated)
                        return Json(new { success = true, id = model.Id });
                    return Json(new { success = false, error = "No se pudo actualizar" });
                }
            }
            catch (Exception ex)
            {
                // Devuelve un error como JSON, conservando el stack trace para logging si lo deseas
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
