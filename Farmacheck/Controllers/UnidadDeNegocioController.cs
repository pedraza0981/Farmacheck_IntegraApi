using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<JsonResult> Guardar([FromForm] UnidadDeNegocio model, IFormFile LogotipoArchivo)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (LogotipoArchivo != null && LogotipoArchivo.Length > 0)
            {
                using var ms = new MemoryStream();
                await LogotipoArchivo.CopyToAsync(ms);
                model.Logotipo = Convert.ToBase64String(ms.ToArray());
            }

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
                existente.Rfc = model.Rfc;
                if (!string.IsNullOrEmpty(model.Logotipo))
                    existente.Logotipo = model.Logotipo;
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
