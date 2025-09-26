using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistScoreRating;
using Farmacheck.Application.Models.ChecklistSections;
using Farmacheck.Models;
using Farmacheck.Models.Inputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Farmacheck.Controllers
{
    public class FormulariosController : Controller
    {
        private static readonly List<SeccionInputModel> _inMemorySections = new();
        private static int _nextSectionId = 1;
        private static int cuestionarioSeleccionado;

        public static List<FormularioViewModel> _formularios = new List<FormularioViewModel>
        {
            new FormularioViewModel { Id = 1, Nombre = "Actividades Día 111", Fecha = new DateTime(2024, 01, 15), EstaActivo = true },
            new FormularioViewModel { Id = 2, Nombre = "Caducidad y Merma - Pruebas22", Fecha  = new DateTime(2024, 03, 01), EstaActivo = false },
            new FormularioViewModel { Id = 3, Nombre = "Check List Prueba22", Fecha = new DateTime(2024, 02, 10), EstaActivo = true }
        };

        public static List<CuestionarioViewModel> _cuestionarios = new List<CuestionarioViewModel>();
        public static List<SeccionViewModel> _secciones = new List<SeccionViewModel>();

        private readonly IChecklistApiClient _apiClient;
        private readonly ICategoryByQuestionnaireApiClient _checklistCategoryApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IChecklistSectionApiClient _seccionApiClient;
        private readonly IMapper _mapper;
        

        public FormulariosController(
            IChecklistApiClient apiClient, 
            ICategoryByQuestionnaireApiClient checklistCategoryApiClient,
            ICategoryApiClient categoryApiClient,
            IChecklistSectionApiClient sectionApiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _checklistCategoryApiClient = checklistCategoryApiClient;
            _categoryApiClient = categoryApiClient;
            _seccionApiClient = sectionApiClient;
            _mapper = mapper;
        }

    #region Checklists
        // GET: /Checklists
        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetAllChecklistsAsync();
            var dtos = _mapper.Map<List<CuestionarioDto>>(apiData);
            _cuestionarios = _mapper.Map<List<CuestionarioViewModel>>(dtos);
            
            return View(_cuestionarios);
        }

        [HttpGet]
        public JsonResult ListarFormularios()
        {
            var lista = _cuestionarios.Select(f => new
            {
                f.Id,
                f.Nombre,
                Fecha = f.CreadoEl.ToString("yyyy-MM-dd"),
                Publicar = f.PublicarCuestionario
            }).OrderBy(c => c.Nombre).ToList();

            return Json(new { success = true, formularios = lista });
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
        public async Task<JsonResult> GuardarCuestionario([FromForm] string data, [FromForm] IFormFile? archivo)
        {
            if (data is null)
                return Json(new { success = false, error = "Capture información del checklist" });

            CuestionarioViewModel model = JsonConvert.DeserializeObject<CuestionarioViewModel>(data);

            if (model is null)
                return Json(new { success = false, error = "Hubo un error al guardar el checklist" });

            model.ArchivoImagen = await ObtenerArchivoBase64(archivo);

            if (model.Id == 0)
            {                
                var request = _mapper.Map<ChecklistRequest>(model);
                request.ClasificacionDePuntaje = _mapper.Map<List<ChecklistScoreRatingRequest>>(ObtenerClasificacionesDePuntaje(model));
                var id = await _apiClient.CreateAsync(request);
                
                model.Id = id;
                model.CreadoEl = DateTime.Now;
                _cuestionarios.Add(model);

                return Json(new { success = true, id, message = "Checklist creado" });
            }

            var updateRequest = _mapper.Map<UpdateChecklistRequest>(model);
            updateRequest.ClasificacionDePuntaje = _mapper.Map<List<UpdateChecklistScoreRatingRequest>>(ObtenerClasificacionesDePuntaje(model));
            var updated = await _apiClient.UpdateAsync(updateRequest);
            
            if (updated)
                return Json(new { success = true, id = model.Id, message = "Checklist actualizado" });

            return Json(new { success = false, error = "No se pudo actualizar" });
        }

        [HttpPost]
        public JsonResult EliminarFormulario(int id)
        {
            var cuestionario = _cuestionarios.FirstOrDefault(f => f.Id == id);
            if (cuestionario == null)
                return Json(new { success = false, error = "Checklist no encontrado" });

            _apiClient.DeleteAsync(id);
            _cuestionarios.Remove(cuestionario);

            return Json(new { success = true });
        }

        // GET: /Formularios/ConfigurarFormulario        
        [HttpGet]
        public async Task<IActionResult> ConfigurarFormulario(int? id)
        {
            CuestionarioViewModel cuestionario;

            if (id.HasValue)
            {
                cuestionario = _cuestionarios.FirstOrDefault(f => f.Id == id.Value);

                if (cuestionario != null)
                {
                    //CargarListasComunes(formulario); // <-- Cargar listas al editar
                    ViewData["Title"] = "Editar Checklist";
                    CargarListasComunes(cuestionario); // <-- Cargar listas al editar
                    return View(cuestionario);
                }
            }

            cuestionario = new CuestionarioViewModel();

            //CargarListasComunes(formulario); // <-- Cargar listas al crear nuevo
            CargarListasComunes(cuestionario); // <-- Cargar listas al editar
            ViewData["Title"] = "Crear Checklist";
            return View(cuestionario);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerFormulario(int id)
        {
            var entidad = await _apiClient.GetChecklistAsync(id);
            if (entidad == null)
                return Json(new { success = false });

            var dto = _mapper.Map<CuestionarioDto>(entidad);
            var model = _mapper.Map<CuestionarioViewModel>(dto);
            var r = model.ClasificacionesDePuntaje.SingleOrDefault(c => c.Etiqueta == "Insuficiente");
            if (model.ClasificacionesDePuntaje != null)
            {
                model.RangoRojo = model.ClasificacionesDePuntaje.SingleOrDefault(c => c.Etiqueta == "Insuficiente")?.PuntajeMaximo;
                model.RangoAmarillo = model.ClasificacionesDePuntaje.SingleOrDefault(c => c.Etiqueta == "Bueno")?.PuntajeMaximo;
                model.RangoVerde = model.ClasificacionesDePuntaje.SingleOrDefault(c => c.Etiqueta == "Excelente")?.PuntajeMaximo;
            }
            
            return Json(new { success = true, data = model });
        }

        private List<ClasificacionDePuntajeViewModel> ObtenerClasificacionesDePuntaje(CuestionarioViewModel model)
        {
            return new List<ClasificacionDePuntajeViewModel>()
            {
                new ClasificacionDePuntajeViewModel { PuntajeMaximo = model.RangoRojo, Etiqueta = "Insuficiente", Color = "#FF0000" },
                new ClasificacionDePuntajeViewModel { PuntajeMaximo = model.RangoAmarillo, Etiqueta = "Bueno", Color = "#008000"  },
                new ClasificacionDePuntajeViewModel { PuntajeMaximo = model.RangoVerde, Etiqueta = "Excelente", Color = "#008000"  }
            };
        }

        #endregion

        #region Categorias
        [HttpGet]
        public async Task<JsonResult> ListarCategorias()
        {
            var apiData = await _checklistCategoryApiClient.GetAllCategoriesAsync();

            var dtos = _mapper.Map<List<CategoryByQuestionnaireDto>>(apiData);
            var categorias = _mapper.Map<List<CategoriaCuestionarioViewModel>>(dtos);

            return Json(new { success = true, data = categorias });
        }

        [HttpGet]
        public async Task<JsonResult> ListarCategoriasDeSecciones()
        {
            var apiData = await _categoryApiClient.GetAllCategoriesAsync();
            var dtos = _mapper.Map<List<CategoriaDto>>(apiData);
            var categorias = _mapper.Map<List<CategoriaViewModel>>(dtos);
            return Json(new { success = true, data = categorias });
        }
        #endregion

        [HttpGet]
        public IActionResult ConfigurarSecciones(int id)
        {
            var formulario = _cuestionarios.FirstOrDefault(f => f.Id == id);
            ViewBag.FormularioId = id;
            ViewBag.NombreFormulario = formulario?.Nombre;
            cuestionarioSeleccionado = id;
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerSeccion(int id)
        {
            var seccion = _secciones.FirstOrDefault(s => s.Id == id);
            if (seccion == null)
                return Json(new { success = false, error = "Sección no encontrada" });

            return Json(new { success = true, data = seccion });
        }

        [HttpPost]
        public async Task<JsonResult> GuardarSeccion([FromBody] SeccionInputModel model)
        {
            if (model is null || string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre y la categoría son obligatorios." });

            if (model.Id == 0)
            {
                var request = _mapper.Map<ChecklistSectionRequest>(model);
                var response = await _seccionApiClient.CreateAsync(request);
                if (response.Id is null)
                {
                    return Json(new { success = false, error = response.Message });
                }

                var id = response.Id;
                return Json(new { success = true, message = "Sección creada", id = model.Id });
            }
            else
            {
                var existente = _secciones.FirstOrDefault(s => s.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "Sección no encontrada." });

                if (existente.Nombre == model.Nombre && existente.CategoriaId == model.CategoriaId)
                {
                    return Json(new { success = true, message = "Sección actualizada" });
                }

                var seccionRequest = new UpdateChecklistSectionRequest() { 
                    CuestionarioId = model.FormularioId, 
                    SeccionId = model.Id, 
                    Nombre = model.Nombre,
                    CategoriaId = model.CategoriaId
                };
                var response = await _seccionApiClient.UpdateAsync(seccionRequest);
                if (!response.Updated)
                {
                    return Json(new { success = false, error = response.Message });
                }

                _secciones.Remove(existente);
                return Json(new { success = true, message = "Sección actualizada" }); 
            }
        }

        [HttpPost]
        public JsonResult EliminarSeccion(int formularioId, int seccionId)
        {
            var cuestionarioId = ViewBag.FormulaioId;
            var seccion = _secciones.FirstOrDefault(s => s.Id == seccionId);
            if (seccion == null)
                return Json(new { success = false, error = "Sección no encontrada" });

            var seccionRequest = new RemoveChecklistSectionRequest() { CuestionarioId = formularioId, SeccionId = seccionId };
            
            _seccionApiClient.DeleteAsync(seccionRequest);
            _secciones.Remove(seccion);

            return Json(new { success = true, message = "Sección eliminada" });
        }

        public static List<CuestionarioViewModel> ObtenerFormularios()
        {
            return _cuestionarios;
        }

        public static SeccionInputModel? ObtenerSeccionPorId(int id)
        {
            var cuestionarioId = cuestionarioSeleccionado;
            var seccion = _secciones.FirstOrDefault(s => s.Id == id);
            var seccionInput = new SeccionInputModel() { Id = seccion.Id, FormularioId = cuestionarioId, Nombre = seccion.Nombre };
            return seccionInput;
        }

        // GET: /Formularios/Previsualizar/5
        public async Task<IActionResult> Previsualizar(int id)
        {
            var apiData = await _apiClient.GetChecklistSummaryAsync(id);
            var dtos = _mapper.Map<ChecklistSummaryDto>(apiData);
            var checklists = _mapper.Map<ChecklistSummaryViewModel>(dtos);
            
            return View(checklists);
        }

        // GET: /Formularios/Editar/5
        public IActionResult Editar(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null) return NotFound();
            return View(formulario);
        }

        // GET: /Formularios/Configurar/5
        public IActionResult Configurar(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null) return NotFound();
            return View(formulario);
        }

        // GET: /Formularios/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null) return NotFound();
            return View(formulario);
        }

        // POST: /Formularios/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario != null)
            {
                _formularios.Remove(formulario);
            }
            return RedirectToAction("Index");
        }

        // GET: /Formularios/Construir/5
        [HttpGet]
        public IActionResult Construir(int id)
        {
            var modelo = new FormularioBuilderViewModel();

            return View("ConstruirFormulario", modelo);
        }

        // --- Simulación de "BD" en memoria para secciones ---


        // GET: /Formularios/ListarSecciones?formularioId=5
        [HttpGet]
        public async Task<JsonResult> ListarSecciones(int formularioId)
        {
            var apiData = await _seccionApiClient.GetSectionsByChecklistAsync(formularioId);
            apiData.Secciones = apiData?.Secciones?.Where(s => s.Estatus == true).OrderBy(s => s.Nombre).ToList();

            var dtos = _mapper.Map<SeccionDelCuestionarioDto>(apiData);
            _secciones = _mapper.Map<List<SeccionViewModel>>(dtos.Secciones);

            return Json(new { success = true, secciones = _secciones });
        }

        // POST: /Formularios/CrearSeccion
        [HttpPost]
        public JsonResult CrearSeccion([FromBody] SeccionInputModel m)
        {
            if (string.IsNullOrWhiteSpace(m.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            // 1) Asignar un Id simulado
            m.Id = _nextSectionId++;

            // 2) Guardar en la lista
            _inMemorySections.Add(m);

            // 3) Devolver el Id recién asignado
            return Json(new { success = true, newId = m.Id });
        }

        [HttpGet]
        public async Task<IActionResult> DescargarReporte(int id)
        {
            var base64 = await _apiClient.GetReport(id);
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteChecklist.xlsx");
        }

        private void CargarListasComunes(CuestionarioViewModel formulario)
        {
            formulario.AuditoresDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Value = "auditor1", Text = "Auditor 1" },
                new SelectListItem { Value = "auditor2", Text = "Auditor 2" }
            };

                    formulario.AuditadosDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Value = "auditado1", Text = "Auditado 1" },
                new SelectListItem { Value = "auditado2", Text = "Auditado 2" }
            };

                    formulario.SupervisoresDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Value = "supervisor1", Text = "Supervisor 1" },
                new SelectListItem { Value = "supervisor2", Text = "Supervisor 2" }
            };

                    formulario.UnidadesDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Value = "ventas", Text = "Ventas" },
                new SelectListItem { Value = "almacen", Text = "Almacén" },
                new SelectListItem { Value = "admin", Text = "Administración" }
            };
        }

        // POST: /Formularios/ConfigurarFormulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfigurarFormulario(FormularioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.Id = _formularios.Max(f => f.Id) + 1;
            modelo.Fecha = DateTime.Now;
            _formularios.Add(modelo);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GuardarFormulario([FromBody] FormularioViewModel model)
        {
            if (model == null)
                return Json(new { success = false, error = "Datos inválidos" });

            if (model.Id == 0)
            {
                model.Id = _formularios.Any() ? _formularios.Max(f => f.Id) + 1 : 1;
                model.Fecha = DateTime.Now;
                _formularios.Add(model);

                return Json(new { success = true, id = model.Id, message = "Formulario creado" });
            }

            var existente = _formularios.FirstOrDefault(f => f.Id == model.Id);
            if (existente == null)
                return Json(new { success = false, error = "Checklist no encontrado" });

            existente.Nombre = model.Nombre;
            existente.Alias = model.Alias;
            existente.EtiquetaCampo = model.EtiquetaCampo;
            existente.EstandarCompania = model.EstandarCompania;
            existente.Auditor = model.Auditor;
            existente.Auditado = model.Auditado;
            existente.Supervisor = model.Supervisor;
            existente.UnidadNegocio = model.UnidadNegocio;
            existente.FirmaObligatoria = model.FirmaObligatoria;
            existente.PersonalizarFirmas = model.PersonalizarFirmas;
            existente.Etiqueta1 = model.Etiqueta1;
            existente.Etiqueta2 = model.Etiqueta2;
            existente.NotifCorreoFinRevision = model.NotifCorreoFinRevision;
            existente.NotifCorreoBajoEstandar = model.NotifCorreoBajoEstandar;
            existente.NotifPushBajoEstandar = model.NotifPushBajoEstandar;
            existente.CorreosAdicionales = model.CorreosAdicionales;
            existente.PushAdicionales = model.PushAdicionales;
            existente.SustituirLogoPDF = model.SustituirLogoPDF;
            existente.SustituirEmpresaPDF = model.SustituirEmpresaPDF;
            existente.PlanesAccionAutomatico = model.PlanesAccionAutomatico;
            existente.Categoria = model.Categoria;
            existente.CaracterInformativo = model.CaracterInformativo;
            existente.OcultarTareasPendientes = model.OcultarTareasPendientes;
            existente.PermitirFotosGaleria = model.PermitirFotosGaleria;
            existente.ActivarGeolocalizacion = model.ActivarGeolocalizacion;
            existente.Publicar = model.Publicar;
            existente.EsconderCalificacion = model.EsconderCalificacion;
            existente.Rutinas = model.Rutinas;
            existente.EstaActivo = model.EstaActivo;

            return Json(new { success = true, id = model.Id, message = "Formulario actualizado" });
        }

        [HttpPost]
        public JsonResult GuardarAsignaciones([FromBody] AsignacionFormularioInputModel model)
        {
            if (model == null || model.FormularioId == 0)
                return Json(new { success = false, error = "Datos inválidos" });

            return Json(new { success = true, message = "Asignaciones guardadas (simulación)" });
        }
    }
}
