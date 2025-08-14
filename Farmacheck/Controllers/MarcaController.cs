using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Brands;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IBrandApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IBusinessUnitApiClient _businessUnitApiClient;

        public MarcaController(IBrandApiClient apiClient, IMapper mapper, IBusinessUnitApiClient businessUnitApiClient)
        {
            _apiClient = apiClient;
            _mapper = mapper;
            _businessUnitApiClient = businessUnitApiClient;
        }

        public async Task<IActionResult> Index(int unidadId = 0)
        {
            ViewBag.UnidadId = unidadId;

            var apiData = await _apiClient.GetAllBrandsAsync();

            // Map first to DTOs and then to the ViewModel to avoid missing configuration
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var items = _mapper.Map<List<MarcaViewModel>>(dtos);

            return View(items);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetAllBrandsAsync();

            // Map first to DTOs and then to the ViewModel to avoid missing configuration
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var items = _mapper.Map<List<MarcaViewModel>>(dtos);

            return Json(new { success = true, data = items });
        }

        [HttpGet]
        public async Task<JsonResult> ListarUnidadesNegocio()
        {
            var apiData = await _businessUnitApiClient.GetBusinessUnitsAsync();
            var dtos = _mapper.Map<List<BusinessUnitDto>>(apiData);
            var unidades = _mapper.Map<List<UnidadDeNegocio>>(dtos);
            return Json(new { success = true, data = unidades });
        }

        

        [HttpGet]
        public async Task<JsonResult> ListarPorUnidadNegocio(int unidadId)
        {
            var apiData = await _apiClient.GetBrandsByBusinessUnitAsync(unidadId);
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

                model.Estatus ??= true;

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

        [HttpGet]
        public async Task<IActionResult> DescargarReporte()
        {
            var base64 = await _apiClient.GetReport();
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteMarcas.xlsx");
        }

    }
}
