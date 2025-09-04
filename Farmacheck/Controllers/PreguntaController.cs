using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.GroupingTags;
using Farmacheck.Application.Models.Questions;
using Farmacheck.Helpers;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;


namespace Farmacheck.Controllers
{
    public class PreguntaController : Controller
    {
        private static List<PreguntaViewModel> _preguntas = new();
        private static List<FormatoDeRespuestaCatViewModel> _formatos = new();
        private static List<EtiquetaDeAgrupacionViewModel> _etiquetas = new();

        private static PreguntaViewModel _preguntaseleccionada = new();
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
        private readonly IResponseFormatCatApiClient _formatsApiClient;
        private readonly IGroupingTagApiClient _groupingTagApiClient;
        private readonly IMapper _mapper;

        public PreguntaController(
            IChecklistApiClient apiClient, 
            IQuestionApiClient questionApiClient, 
            IChecklistSectionApiClient sectionApiClient, 
            IResponseFormatCatApiClient formatsApiClient,
            IGroupingTagApiClient groupingTagApiClient, IMapper mapper)
        {
            _questionApiClient = questionApiClient;
            _seccionApiClient = sectionApiClient;
            _formatsApiClient = formatsApiClient;
            _groupingTagApiClient = groupingTagApiClient;
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

            var formatsData = await _formatsApiClient.GetAllFormatsAsync();
            var formatosDto = _mapper.Map<List<FormatoDeRespuestaCatDto>>(formatsData);
            _formatos = _mapper.Map<List<FormatoDeRespuestaCatViewModel>>(formatosDto);

            ViewBag.FormularioId = formulario.Id;
            ViewBag.SeccionId = seccion.Id;
            ViewBag.NombreFormulario = formulario.Nombre;
            ViewBag.TiposPregunta = _formatos;
            ViewBag.Formatos = JsonConvert.SerializeObject(_formatos);

            var etiquetas = await _groupingTagApiClient.GetTagsBySection(formulario.Id, seccion.Id);
            var etiquetasDto = _mapper.Map<List<EtiquetaDeAgrupacionDto>>(etiquetas);
            _etiquetas = _mapper.Map<List<EtiquetaDeAgrupacionViewModel>>(etiquetasDto);
            ViewBag.Etiquetas = _etiquetas;

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

                if (_preguntaseleccionada.FormatoDeRespuesta.FormatoId != model.FormatoDeRespuesta.FormatoId)
                {
                    var opciones = model.OpcionesPorPregunta != null ? model.OpcionesPorPregunta : Enumerable.Empty<OpcionesPorPreguntaViewModel>();
                    var etiquetas = model.EtiquetasPorEscalaNumerica != null ? model.EtiquetasPorEscalaNumerica : Enumerable.Empty<EtiquetasPorEscalaNumericaViewModel>();

                    if (_preguntaseleccionada.OpcionesPorPregunta != null)
                    {
                        model.OpcionesPorPregunta = new();
                        foreach (var op in _preguntaseleccionada.OpcionesPorPregunta)
                        {
                            op.Estatus = false;
                            model.OpcionesPorPregunta.Add(op);
                        }
                    }

                    if (_preguntaseleccionada.EtiquetasPorEscalaNumerica != null)
                    {
                        model.EtiquetasPorEscalaNumerica = new();
                        foreach (var et in _preguntaseleccionada.EtiquetasPorEscalaNumerica)
                        {
                            et.Estatus = false;
                            model.EtiquetasPorEscalaNumerica.Add(et);
                        }
                    }

                    model.OpcionesPorPregunta?.AddRange(opciones);
                    model.EtiquetasPorEscalaNumerica?.AddRange(etiquetas);
                } else
                {
                    if (model.EtiquetasPorEscalaNumerica != null && CatalogosStaticos.PreguntasPorEscala.Exists(ft => ft.Value == _preguntaseleccionada.FormatoDeRespuesta.FormatoId.ToString()))
                    {
                        model.EtiquetasPorEscalaNumerica[0].Id = _preguntaseleccionada.EtiquetasPorEscalaNumerica[0].Id;
                    }
                }

                if (_preguntaseleccionada.OpcionesPorPregunta != null)
                {
                    foreach (var opcion in _preguntaseleccionada?.OpcionesPorPregunta ?? Enumerable.Empty<OpcionesPorPreguntaViewModel>())
                    {
                        var op = model?.OpcionesPorPregunta?.SingleOrDefault(op => op.Etiqueta == opcion.Etiqueta);
                        if (op is null)
                        {
                            opcion.Estatus = false;
                            model?.OpcionesPorPregunta?.Add(opcion);
                        }
                    }
                }

                var request = _mapper.Map<UpdateQuestionRequest>(model);
                var result = await _questionApiClient.UpdateAsync(request);

                return Json(new { success = true, message = "Checklist actualizado" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GuardarEtiqueta([FromBody] EtiquetaDeAgrupacionViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Etiqueta))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.Id == 0)
            {
                var request = _mapper.Map<GroupingTagRequest>(model);
                var id = await _groupingTagApiClient.CreateAsync(request);
                if (id == 0)
                {
                    return Json(new { success = false, error = "Hubo un error al guardar la etiqueta" });
                }

                var etiquetas = await _groupingTagApiClient.GetTagsBySection(model.CuestionarioId, model.SeccionId);
                var etiquetasDto = _mapper.Map<List<EtiquetaDeAgrupacionDto>>(etiquetas);
                _etiquetas = _mapper.Map<List<EtiquetaDeAgrupacionViewModel>>(etiquetasDto);
                ViewBag.Etiquetas = _etiquetas;

                return Json(new { success = true, message = "Etiqueta creada", id = model.Id });
            }
            else
            {
                var seccionRequest = new UpdateGroupingTagRequest() { Id = model.Id, CuestionarioId = model.CuestionarioId, SeccionId = model.SeccionId, Etiqueta = model.Etiqueta };
                var updated = await _groupingTagApiClient.UpdateAsync(seccionRequest);

                if (!updated)
                {
                    return Json(new { success = false, error = "Hubo un error al guardar la etiqueta" });
                }

                return Json(new { success = true, message = "Etiqueta actualizada" });
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
            _preguntaseleccionada = _mapper.Map<PreguntaViewModel>(dto);
            
            return Json(new { success = true, data = _preguntaseleccionada });
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
