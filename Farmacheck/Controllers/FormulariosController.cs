using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Farmacheck.Models;
using Farmacheck.Models.Inputs;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Farmacheck.Controllers
{
    public class FormulariosController : Controller
    {
        private static readonly List<SeccionInputModel> _inMemorySections = new();
        private static int _nextSectionId = 1;

        public static List<FormularioViewModel> _formularios = new List<FormularioViewModel>
        {
            new FormularioViewModel { Id = 1, Nombre = "Actividades Día 1", Fecha = new DateTime(2024, 01, 15), EstaActivo = true },
            new FormularioViewModel { Id = 2, Nombre = "Caducidad y Merma - Pruebas", Fecha  = new DateTime(2024, 03, 01), EstaActivo = false },
            new FormularioViewModel { Id = 3, Nombre = "Check List Prueba", Fecha = new DateTime(2024, 02, 10), EstaActivo = true }
        };

        // GET: /Formularios
        public IActionResult Index()
        {
            return View(_formularios);
        }

        [HttpGet]
        public IActionResult ConfigurarSecciones(int id)
        {
            ViewBag.FormularioId = id;
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerSeccion(int id)
        {
            var seccion = _inMemorySections.FirstOrDefault(s => s.Id == id);
            if (seccion == null)
                return Json(new { success = false, error = "Sección no encontrada" });

            return Json(new { success = true, data = seccion });
        }

        [HttpPost]
        public JsonResult GuardarSeccion([FromBody] SeccionInputModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Titulo))
                return Json(new { success = false, error = "El título es obligatorio." });

            if (model.Id == 0)
            {
                // Nuevo
                model.Id = _nextSectionId++;
                _inMemorySections.Add(model);
                return Json(new { success = true, message = "Sección creada", id = model.Id });
            }
            else
            {
                // Editar
                var existente = _inMemorySections.FirstOrDefault(s => s.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "Sección no encontrada." });

                existente.Titulo = model.Titulo;
                existente.ActivarNA = model.ActivarNA;
                existente.Descripcion = model.Descripcion;
                existente.RangoVerde = model.RangoVerde;
                existente.RangoAmarillo = model.RangoAmarillo;
                existente.RangoRojo = model.RangoRojo;

                return Json(new { success = true, message = "Sección actualizada" });
            }
        }

        [HttpPost]
        public JsonResult EliminarSeccion(int id)
        {
            var seccion = _inMemorySections.FirstOrDefault(s => s.Id == id);
            if (seccion == null)
                return Json(new { success = false, error = "Sección no encontrada" });

            _inMemorySections.Remove(seccion);
            return Json(new { success = true, message = "Sección eliminada" });
        }

        public static List<FormularioViewModel> ObtenerFormularios()
        {
            return _formularios;
        }

        public static SeccionInputModel? ObtenerSeccionPorId(int id)
        {
            return _inMemorySections.FirstOrDefault(s => s.Id == id);
        }










        // GET: /Formularios/Previsualizar/5
        public IActionResult Previsualizar(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null) return NotFound();
            return View(formulario);
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
            var modelo = new FormularioBuilderViewModel
            {
                FormularioId = id,
                NombreFormulario = "Prueba Apertura",
                Secciones = new List<SeccionViewModel>
                {
                    new SeccionViewModel
                    {
                        Id           = 1,
                        Titulo       = "APERTURA",
                        ActivarNA    = false,
                        Descripcion  = "Chequeo inicial",
                        RangoVerde   = "0–5",
                        RangoAmarillo= "6–10",
                        RangoRojo    = "11–15",
                        Preguntas = new List<PreguntaViewModel>
                        {
                            new PreguntaViewModel { Id = 101, Titulo = "¿La farmacia abrió a tiempo con anuncios apagados?" },
                            new PreguntaViewModel { Id = 102, Titulo = "¿Se colocó el material POP?" }
                        }
                    }
                }
            };

            return View("ConstruirFormulario", modelo);
        }

        // --- Simulación de "BD" en memoria para secciones ---


        // GET: /Formularios/ListarSecciones?formularioId=5
        [HttpGet]
        public JsonResult ListarSecciones(int formularioId)
        {
            var lista = _inMemorySections
                .Where(s => s.FormularioId == formularioId)
                .ToList();
            return Json(new { success = true, secciones = lista });
        }

        // POST: /Formularios/CrearSeccion
        [HttpPost]
        public JsonResult CrearSeccion([FromBody] SeccionInputModel m)
        {
            if (string.IsNullOrWhiteSpace(m.Titulo))
                return Json(new { success = false, error = "El título es obligatorio." });

            // 1) Asignar un Id simulado
            m.Id = _nextSectionId++;

            // 2) Guardar en la lista
            _inMemorySections.Add(m);

            // 3) Devolver el Id recién asignado
            return Json(new { success = true, newId = m.Id });
        }

        private void CargarListasComunes(FormularioViewModel formulario)
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

        // GET: /Formularios/ConfigurarFormulario        
        [HttpGet]
        public IActionResult ConfigurarFormulario(int? id)
        {
            FormularioViewModel formulario;

            if (id.HasValue)
            {
                formulario = _formularios.FirstOrDefault(f => f.Id == id.Value);

                if (formulario != null)
                {
                    CargarListasComunes(formulario); // <-- Cargar listas al editar
                    ViewData["Title"] = "Editar Formulario";
                    return View(formulario);
                }
            }

            formulario = new FormularioViewModel
            {
                Fecha = DateTime.Now,
                EstaActivo = true
            };

            CargarListasComunes(formulario); // <-- Cargar listas al crear nuevo
            ViewData["Title"] = "Crear Formulario";
            return View(formulario);
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

        [HttpGet]
        public JsonResult ListarFormularios()
        {
            var lista = _formularios.Select(f => new
            {
                f.Id,
                f.Nombre,
                Fecha = f.Fecha.ToString("yyyy-MM-dd"),
                f.Publicar
            }).ToList();

            return Json(new { success = true, formularios = lista });
        }

        [HttpGet]
        public JsonResult ObtenerFormulario(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null)
                return Json(new { success = false });

            return Json(new { success = true, data = formulario });
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
                return Json(new { success = false, error = "Formulario no encontrado" });

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
        public JsonResult EliminarFormulario(int id)
        {
            var formulario = _formularios.FirstOrDefault(f => f.Id == id);
            if (formulario == null)
                return Json(new { success = false, error = "Formulario no encontrado" });

            _formularios.Remove(formulario);
            return Json(new { success = true });
        }
    }
}
