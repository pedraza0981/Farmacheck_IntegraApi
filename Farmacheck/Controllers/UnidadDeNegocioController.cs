using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class UnidadDeNegocioController : Controller
    {
        private static readonly List<UnidadDeNegocio> _unidades = new();
        private static int _nextId = 1;

        public IActionResult Index()
        {
            return View(_unidades);
        }

        [HttpGet]
        public JsonResult Listar()
        {
            return Json(new { success = true, data = _unidades });
        }

        [HttpGet]
        public JsonResult Obtener(int id)
        {
            var entidad = _unidades.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            return Json(new { success = true, data = entidad });
        }

        [HttpPost]
        public JsonResult Guardar([FromBody] UnidadDeNegocio model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.Id == 0)
            {
                model.Id = _nextId++;
                _unidades.Add(model);
            }
            else
            {
                var existente = _unidades.FirstOrDefault(x => x.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "No encontrado" });

                existente.Nombre = model.Nombre;
            }

            return Json(new { success = true, id = model.Id });
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var entidad = _unidades.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            _unidades.Remove(entidad);
            return Json(new { success = true });
        }
    }
}
