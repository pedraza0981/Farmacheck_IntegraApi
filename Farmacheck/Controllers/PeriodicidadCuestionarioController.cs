using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class PeriodicidadCuestionarioController : Controller
    {
        private readonly IPeriodicityByQuestionnaireApiClient _apiClient;
        private readonly IChecklistApiClient _checklistApiClient;
        private readonly IMapper _mapper;
        private static readonly Dictionary<int, string> _frecuencias = new()
        {
            { 1, "Diario" },
            { 2, "Semanal" },
            { 3, "Quincenal" },
            { 4, "Mensual" }
        };

        public PeriodicidadCuestionarioController(IPeriodicityByQuestionnaireApiClient apiClient,
            IChecklistApiClient checklistApiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _checklistApiClient = checklistApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetPeriodicitiesAsync();
            var dtos = _mapper.Map<List<PeriodicityByQuestionnaireDto>>(apiData);
            var items = _mapper.Map<List<PeriodicidadCuestionarioViewModel>>(dtos);
            foreach (var item in items)
            {
                item.FrecuenciaDescripcion = _frecuencias.TryGetValue(item.Frecuencia, out var desc)
                    ? desc
                    : item.Frecuencia.ToString();
            }
            return View(items);
        }

        [HttpGet]
        public async Task<JsonResult> FormulariosDisponibles()
        {
            var periodicidades = await _apiClient.GetPeriodicitiesAsync();
            var usados = periodicidades.Select(p => p.CuestionarioId).ToHashSet();
            var formularios = await _checklistApiClient.GetAllChecklistsAsync();
            var disponibles = formularios
                .Where(f => !usados.Contains(f.Id))
                .Select(f => new { f.Id, f.Nombre });
            return Json(new { success = true, data = disponibles });
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetPeriodicitiesAsync();
            var dtos = _mapper.Map<List<PeriodicityByQuestionnaireDto>>(apiData);
            var items = _mapper.Map<List<PeriodicidadCuestionarioViewModel>>(dtos);
            var result = items.Select(i => new
            {
                i.CuestionarioId,
                Frecuencia = _frecuencias.TryGetValue(i.Frecuencia, out var desc) ? desc : i.Frecuencia.ToString(),
                i.Meta
            });
            return Json(new { success = true, data = result });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetPeriodicityAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<PeriodicityByQuestionnaireDto>(entidad);
            var model = _mapper.Map<PeriodicidadCuestionarioViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] PeriodicidadCuestionarioViewModel model)
        {
            try
            {
                var request = _mapper.Map<PeriodicityByQuestionnaireRequest>(model);
                var id = await _apiClient.CreateAsync(request);
                return Json(new { success = true, id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            try
            {
                await _apiClient.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }
    }
}
