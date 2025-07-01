using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class SubMarcaController : Controller
    {
        private static readonly List<SubMarca> _submarcas = new();
        private static int _nextId = 1;

        public IActionResult Index(int marcaId)
        {
            ViewBag.MarcaId = marcaId;
            var lista = _submarcas.Where(s => s.MarcaId == marcaId).ToList();
            return View(lista);
        }

        [HttpGet]
        public JsonResult Listar(int marcaId)
        {
            var lista = _submarcas.Where(s => s.MarcaId == marcaId).ToList();
            return Json(new { success = true, data = lista });
        }

        [HttpGet]
        public JsonResult Obtener(int id)
        {
            var entidad = _submarcas.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            return Json(new { success = true, data = entidad });
        }

        [HttpPost]
        public JsonResult Guardar([FromBody] SubMarca model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.Id == 0)
            {
                model.Id = _nextId++;
                _submarcas.Add(model);
            }
            else
            {
                var existente = _submarcas.FirstOrDefault(x => x.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "No encontrado" });

                existente.Nombre = model.Nombre;
                existente.MarcaId = model.MarcaId;
            }

            return Json(new { success = true, id = model.Id });
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var entidad = _submarcas.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            _submarcas.Remove(entidad);
            return Json(new { success = true });
        }
    }
}
