using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.QuizAssignmentManager;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class AsignacionCuestionarioController : Controller
    {
        private readonly IQuizAssignmentManagerApiClient _apiClient;
        private readonly IMapper _mapper;

        public AsignacionCuestionarioController(IQuizAssignmentManagerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int cuestionarioId)
        {
            var response = await _apiClient.GetQuizAssignmentAsync(cuestionarioId);
            if (response == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<QuizAssignmentManagerDto>(response);
            var model = _mapper.Map<AsignacionCuestionarioViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] AsignacionCuestionarioViewModel model)
        {
            if (model == null || model.CuestionarioId <= 0)
                return Json(new { success = false, error = "Datos inválidos" });

            var request = _mapper.Map<QuizAssignmentManagerRequest>(model);
            var result = await _apiClient.CreateAsync(request);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar([FromBody] AsignacionCuestionarioViewModel model)
        {
            var result = await _apiClient.DeleteAsync(model.CuestionarioId, model.AsignacionPorSupervisor, model.AsignacionDeAuditados, model.AsignacionPorAuditor);
            return Json(new { success = result });
        }
    }
}
