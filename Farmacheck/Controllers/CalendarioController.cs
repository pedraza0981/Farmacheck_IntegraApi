using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Calendario;
using Farmacheck.Helpers;
using Farmacheck.Models;
using Farmacheck.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Farmacheck.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly ICalendarioClient _service;
        private readonly IMapper _mapper;
        private readonly IAuthApiClient _apiClient;


        public CalendarioController(ICalendarioClient service, IMapper mapper, IAuthApiClient apiClient)
        {
            _apiClient = apiClient;
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FullCalendarEventResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var anio = DateTime.Now.Year;
            var usuarioId = 1;

            var result = await _service.GetNonRecurringAsync(usuarioId, anio);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Configuracion(int usuarioId, int calendarioId)
        {
            var result = await _service.GetConfig(usuarioId, calendarioId);
            return Ok(result);
        }

        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> SaveConfiguracion([FromBody] ConfiguracionCalendarioResponse request)
        {
            if (request == null)
                return BadRequest("Body vacío o JSON inválido.");

            // Sella fechas si no vienen; el backend puede ignorarlas si prefiere
            request.ActualizadoEn = request.ActualizadoEn == default ? DateTime.Now : request.ActualizadoEn;
            request.CreadoEn = request.CreadoEn == default ? DateTime.Now : request.CreadoEn;

            var result = await _service.PutConfig(request); // retorna int
            return Ok(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> CrearEvento([FromBody] AddEventoCalendarRequest request)
        {
            var result = await _service.AddEventoAsync(request);
            return Ok(result);
        }

    }
}
