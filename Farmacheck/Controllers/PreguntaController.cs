using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Questions;
using Farmacheck.Helpers;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Farmacheck.Controllers
{
    public class PreguntaController : Controller
    {
        private static List<PreguntaViewModel> _preguntas = new();
        private static int _nextId = 1;

        private static readonly List<CuestionarioViewModel> _formularios = FormulariosController.ObtenerFormularios(); // Usa un getter si es privado

        public static List<SelectListItem> TiposPregunta = new()
        {
            new SelectListItem { Value = "1", Text = "Opción Múltiple" },
            new SelectListItem { Value = "2", Text = "Respuesta Abierta" },
            new SelectListItem { Value = "3", Text = "Escala Numérica" }
        };

        public static List<SelectListItem> Prioridades = new()
        {
            new SelectListItem { Value = "1", Text = "Alta" },
            new SelectListItem { Value = "2", Text = "Media" },
            new SelectListItem { Value = "3", Text = "Baja" }
        };

        private readonly IChecklistSectionApiClient _seccionApiClient;
        private readonly IQuestionApiClient _questionApiClient;
        private readonly IMapper _mapper;

        public PreguntaController(IChecklistApiClient apiClient, IQuestionApiClient questionApiClient, IChecklistSectionApiClient sectionApiClient, IMapper mapper)
        {
            _questionApiClient = questionApiClient;
            _seccionApiClient = sectionApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int seccionId)
        {
            var seccion = FormulariosController.ObtenerSeccionPorId(seccionId);
            
            if (seccion == null) 
                return NotFound();
            
            var formulario = _formularios.FirstOrDefault(f => f.Id == seccion.FormularioId);
             
            if (formulario == null) 
                return NotFound();

            ViewBag.FormularioId = formulario.Id;
            ViewBag.SeccionId = seccion.Id;
            ViewBag.NombreFormulario = formulario.Nombre;

            // Catálogos para selects
            ViewBag.TiposPregunta = CatalogosStaticos.TiposPregunta;
            ViewBag.Prioridades = CatalogosStaticos.Prioridades;

            var apiData = await _seccionApiClient.GetQuestionsBySectionAsync(formulario.Id, seccionId);
            var dtos = _mapper.Map<PreguntasPorSeccionDto>(apiData);
            var preguntas = dtos.Preguntas?.Where(p => p.Estatus == true);
            _preguntas = _mapper.Map<List<PreguntaViewModel>>(preguntas);

            return View(_preguntas);
        }


        public IActionResult Crear(int seccionId)
        {
            ViewBag.TiposPregunta = TiposPregunta;
            ViewBag.Prioridades = Prioridades;
            ViewBag.FormularioId = seccionId;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] PreguntaViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El título es obligatorio." });

            if (model.Id == 0)
            {
                var request = _mapper.Map<QuestionRequest>(model);
                var id = await _questionApiClient.CreateAsync(request);

                model.Id = id;
                _preguntas.Add(model);

                return Json(new { success = true, id, message = "Checklist creado" });
            }
            else
            {
                var existente = _preguntas.FirstOrDefault(p => p.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "No se encontró la pregunta a editar." });

                var request = _mapper.Map<UpdateQuestionRequest>(model);
                var result = await _questionApiClient.UpdateAsync(request);

                return Json(new { success = true, message = "Checklist actualizado" });
            }
        }

        public IActionResult Editar(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null) return NotFound();
            return View(pregunta);
        }

        [HttpPost]
        public IActionResult Editar(PreguntaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var original = _preguntas.FirstOrDefault(p => p.Id == model.Id);
            if (original == null) return NotFound();

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null) return NotFound();

            return View(pregunta);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta != null)
                _preguntas.Remove(pregunta);

            return RedirectToAction("Index");
        }

        public IActionResult Visualizar(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null) return NotFound();

            return View(pregunta);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerPregunta(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null)
                return Json(new { success = false, error = "Pregunta no encontrada" });

            var entidad = await _questionApiClient.GetQuestionAsync(pregunta.CuestionarioId, pregunta.SeccionDelCuestionarioId, pregunta.Id);
            if (entidad == null)
                return Json(new { success = false });

            var dto = _mapper.Map<PreguntaDto>(entidad);
            var model = _mapper.Map<PreguntaViewModel>(dto);
           
            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> EliminarPregunta(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null)
                return Json(new { success = false, error = "Pregunta no encontrada" });

            var preguntaRequest = new RemoveQuestionRequest() { CuestionarioId = pregunta.CuestionarioId, SeccionDelCuestionarioId = pregunta.SeccionDelCuestionarioId, Id = pregunta.Id };

            await _questionApiClient.DeleteAsync(preguntaRequest);
            _preguntas.Remove(pregunta);

            return Json(new { success = true });
        }
    }

}
