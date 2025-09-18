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
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            ViewBag.NombreSeccion = seccion.Nombre;
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

        private async Task<string> ObtenerArchivoBase64(IFormFile? archivo)
        {
            var archivoBase64 = "";

            if (archivo != null && archivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await archivo.CopyToAsync(ms);
                archivoBase64 = Convert.ToBase64String(ms.ToArray());
            }

            return archivoBase64;
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromForm] string data, [FromForm] IFormFile? archivo)
        {
            if (data is null)
                return Json(new { success = false, error = "Capture información de la pregunta" });

            PreguntaViewModel model = JsonConvert.DeserializeObject<PreguntaViewModel>(data);

            if (model is null)
                return Json(new { success = false, error = "Hubo un error al guardar la pregunta" });

            model.ArchivoImagen = await ObtenerArchivoBase64(archivo);

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

                var pregunta = AplicarValidationesAPropiedades(_preguntaseleccionada, model);
                var request = _mapper.Map<UpdateQuestionRequest>(pregunta);
                var result = await _questionApiClient.UpdateAsync(request);

                return Json(new { success = true, message = "Checklist actualizado" });
            }
        }

        private PreguntaViewModel AplicarValidationesAPropiedades(PreguntaViewModel preguntaSeleccionada, PreguntaViewModel pregunta)
        {
            if (preguntaSeleccionada.FormatoDeRespuesta.FormatoId != pregunta.FormatoDeRespuesta.FormatoId)
            {
                var opciones = pregunta.OpcionesPorPregunta != null ? pregunta.OpcionesPorPregunta : Enumerable.Empty<OpcionesPorPreguntaViewModel>();
                var etiquetas = pregunta.EtiquetasPorEscalaNumerica != null ? pregunta.EtiquetasPorEscalaNumerica : Enumerable.Empty<EtiquetasPorEscalaNumericaViewModel>();

                if (preguntaSeleccionada.OpcionesPorPregunta != null)
                {
                    pregunta.OpcionesPorPregunta = new();
                    foreach (var op in preguntaSeleccionada.OpcionesPorPregunta)
                    {
                        op.Estatus = false;
                        pregunta.OpcionesPorPregunta.Add(op);
                    }
                }

                if (preguntaSeleccionada.EtiquetasPorEscalaNumerica != null)
                {
                    pregunta.EtiquetasPorEscalaNumerica = new();
                    foreach (var et in preguntaSeleccionada.EtiquetasPorEscalaNumerica)
                    {
                        et.Estatus = false;
                        pregunta.EtiquetasPorEscalaNumerica.Add(et);
                    }
                }

                pregunta.OpcionesPorPregunta?.AddRange(opciones);
                pregunta.EtiquetasPorEscalaNumerica?.AddRange(etiquetas);
            }
            else
            {
                if (pregunta.EtiquetasPorEscalaNumerica != null && CatalogosStaticos.PreguntasPorEscala.Exists(ft => ft.Value == preguntaSeleccionada.FormatoDeRespuesta.FormatoId.ToString()))
                {
                    pregunta.EtiquetasPorEscalaNumerica[0].Id = preguntaSeleccionada.EtiquetasPorEscalaNumerica[0].Id;
                }
            }

            if (preguntaSeleccionada.OpcionesPorPregunta != null)
            {
                foreach (var opcion in preguntaSeleccionada?.OpcionesPorPregunta ?? Enumerable.Empty<OpcionesPorPreguntaViewModel>())
                {
                    var op = pregunta?.OpcionesPorPregunta?.SingleOrDefault(op => op.Etiqueta == opcion.Etiqueta);
                    if (op is null)
                    {
                        opcion.Estatus = false;
                        pregunta?.OpcionesPorPregunta?.Add(opcion);
                    }
                }
            }

            return pregunta;
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
