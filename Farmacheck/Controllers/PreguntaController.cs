using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Farmacheck.Models;
using Farmacheck.Models.Inputs;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Farmacheck.Helpers;

namespace Farmacheck.Controllers
{
    public class PreguntaController : Controller
    {
        private static readonly List<PreguntaViewModel> _preguntas = new();
        private static int _nextId = 1;

        private static readonly List<FormularioViewModel> _formularios = FormulariosController.ObtenerFormularios(); // Usa un getter si es privado

        public static List<SelectListItem> TiposPregunta = new()
        {
            new SelectListItem { Value = "1", Text = "Opción Múltiple" },
            new SelectListItem { Value = "2", Text = "Respuesta Abierta" }
        };

                public static List<SelectListItem> Prioridades = new()
        {
            new SelectListItem { Value = "1", Text = "Alta" },
            new SelectListItem { Value = "2", Text = "Media" },
            new SelectListItem { Value = "3", Text = "Baja" }
        };


        public IActionResult Index(int seccionId)
        {
            var seccion = FormulariosController.ObtenerSeccionPorId(seccionId);
            if (seccion == null) return NotFound();
            var formulario = _formularios.FirstOrDefault(f => f.Id == seccion.FormularioId);
            if (formulario == null) return NotFound();

            ViewBag.FormularioId = formulario.Id;
            ViewBag.SeccionId = seccion.Id;
            ViewBag.NombreFormulario = formulario.Nombre;

            // Catálogos para selects
            ViewBag.TiposPregunta = CatalogosStaticos.TiposPregunta;
            ViewBag.Prioridades = CatalogosStaticos.Prioridades;

            var preguntasDelFormulario = _preguntas
                .Where(p => p.SeccionId == seccionId)
                .ToList();

            return View(preguntasDelFormulario);
        }


        public IActionResult Crear(int seccionId)
        {
            ViewBag.TiposPregunta = TiposPregunta;
            ViewBag.Prioridades = Prioridades;
            ViewBag.FormularioId = seccionId;

            return View(new PreguntaViewModel { SeccionId = seccionId });
        }

        [HttpPost]
        public JsonResult Guardar([FromBody] PreguntaViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Titulo))
                return Json(new { success = false, error = "El título es obligatorio." });

            // Si es nueva
            if (model.Id == 0)
            {
                model.Id = _nextId++;
                _preguntas.Add(model);
            }
            else
            {
                var existente = _preguntas.FirstOrDefault(p => p.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "No se encontró la pregunta a editar." });

                // Actualiza propiedades
                existente.Titulo = model.Titulo;
                existente.Descripcion = model.Descripcion;
                existente.TipoPreguntaId = model.TipoPreguntaId;
                existente.EsRequerido = model.EsRequerido;
                existente.PrioridadId = model.PrioridadId;
                existente.Hipervinculo = model.Hipervinculo;
                existente.AgregarComentario = model.AgregarComentario;
                existente.AgregarImagen = model.AgregarImagen;
                existente.AgregarCamposExtras = model.AgregarCamposExtras;
                existente.SeccionId = model.SeccionId;
                existente.Opciones = model.Opciones;
            }

            return Json(new { success = true });
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

            original.Titulo = model.Titulo;
            original.TipoPregunta = model.TipoPregunta;

            return RedirectToAction("Index", new { seccionId = model.SeccionId });
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

            return RedirectToAction("Index", new { seccionId = pregunta?.SeccionId });
        }

        public IActionResult Visualizar(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null) return NotFound();

            return View(pregunta);
        }

        [HttpGet]
        public JsonResult ObtenerPregunta(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null)
                return Json(new { success = false, error = "Pregunta no encontrada" });

            return Json(new { success = true, data = pregunta });
        }

        [HttpPost]
        public JsonResult EliminarPregunta(int id)
        {
            var pregunta = _preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null)
                return Json(new { success = false, error = "Pregunta no encontrada" });

            _preguntas.Remove(pregunta);
            return Json(new { success = true });
        }
    }

}
