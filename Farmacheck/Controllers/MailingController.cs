using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.MailingProgramacion;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Farmacheck.Controllers
{
    public class MailingController : Controller
    {
        private readonly IMailingProgramacionClient _mailingClient;
        private readonly IUserApiClient _apiClient;
        private readonly IMapper _mapper;

        public MailingController(IMailingProgramacionClient mailingClient, IUserApiClient apiClient, IMapper mapper)
        {
            _mailingClient = mailingClient;
            _apiClient = apiClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiData = await _mailingClient.GetAllAsync();
            var programaciones = _mapper.Map<List<vMailingProgramacionWebDto>>(apiData);

            var usersApi = await _apiClient.GetAllUsersAsync();
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(
                              _mapper.Map<List<UserDto>>(usersApi));

            var model = new MailingProgramacionIndexVM
            {
                Programaciones = programaciones,
                UsuariosDisponibles = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Nombre} {u.ApellidoPaterno} {u.ApellidoMaterno}"
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> ListarUsuario()
        {
            var apiData = await _apiClient.GetAllUsersAsync();

            var dtos = _mapper.Map<List<UserDto>>(apiData);
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(dtos);

            return Json(new { success = true, data = usuarios });
        }

        [HttpGet]
        public async Task<IActionResult> Creates()
        {
            var apiData = await _apiClient.GetAllUsersAsync();
            var dtos = _mapper.Map<List<UserDto>>(apiData);
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(dtos);

            var model = new MailingProgramacionIndexVM
            {
                UsuariosDisponibles = usuarios.Select(u =>
                    new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Nombre + " " + u.ApellidoPaterno + " " + u.ApellidoMaterno
                    }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> DestinatarioProgramacion(long id)
        {
            var apiData = await _mailingClient.DestinatarioProgramacion(id);

            var model = apiData.Where(x => x.UsuarioId == null || x.UsuarioId == 0).ToList();

            return Json(new { success = true, data = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MailingProgramacionIndexVM model)
        {
            if (!ModelState.IsValid)
            {
                // volver a armar model.Programaciones si regresas a la vista
                var apiData = await _mailingClient.GetAllAsync();
                model.Programaciones = _mapper.Map<List<vMailingProgramacionWebDto>>(apiData);
                return View("Index", model);
            }

            // model.SupervisoresIds => lista de enteros seleccionados
            // TODO: guardar/usar estos IDs
            // await _servicio.Guardar(..., model.SupervisoresIds);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _mailingClient.GetAllAsync();
            var mailingl = _mapper.Map<List<vMailingProgramacionWebDto>>(apiData);

            return Json(new { success = true, data = mailingl });
        }

        // GET: MailingController/Details/5
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Details(int id)
        {
            var apiData = await _mailingClient.Details(id);
            var mailingl = _mapper.Map<MailingProgramacionRequest>(apiData);

            return Json(new { success = true, data = mailingl });
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] MailingProgramacionRequest model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Datos inválidos." });

            var hora = model.HoraEnvio;

            // Validaciones por periodicidad
            if (model.Periodicidad_id == 3 && model.DiaSemana is null)
                return Json(new { success = false, message = "Selecciona un día de la semana." });

            if (model.Periodicidad_id == 4 && model.DiaMes is null)
                return Json(new { success = false, message = "Selecciona un día del mes." });

            try
            {
                // Devuelve el ID del nuevo registro (int)
                var id = await _mailingClient.CreateAsync(model);

                var ok = id > 0;
                return Json(new
                {
                    success = ok,
                    id,
                    message = ok ? "Guardado correctamente." : "No se pudo guardar."
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar: " + ex.Message });
            }
        }

        // GET: MailingController/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var apiData = await _mailingClient.DeleteAsync(id);
                return Json(new { success = apiData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al eliminar el rol: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> ZonaHorario()
        {
            var apiData = await _mailingClient.GetZonaHorarioAsync();
            var dtos = _mapper.Map<List<ZonaHorariaDto>>(apiData);

            var data = dtos.Select(z => new
            {
                id = z.Id,
                ianaName = z.IanaName
            });

            return Json(new { success = true, data });
        }

        [HttpGet]
        public async Task<JsonResult> Periodicidad()
        {
            var apiData = await _mailingClient.GetPeriodicidadAsync();
            var dtos = _mapper.Map<List<PeriodicidadDto>>(apiData);

            var data = dtos.Select(z => new
            {
                id = z.Id,
                descripcion = z.Descripcion
            });

            return Json(new { success = true, data });
        }

        [HttpGet]
        public async Task<JsonResult> TipoReporte()
        {
            var apiData = await _mailingClient.GetTipoReporteAsync();
            var dtos = _mapper.Map<List<TipoReporteDto>>(apiData);

            var data = dtos.Select(z => new
            {
                id = z.Id,
                descripcion = z.Descripcion
            });

            return Json(new { success = true, data });
        }

        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] MailingProgramacionRequest model, int id)
        {
            try
            {
                var apiData = await _mailingClient.EditarAsync(model, id);
                return Json(new { success = apiData });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al actualizar el rol: " + ex.Message });
            }
        }

    }
}
