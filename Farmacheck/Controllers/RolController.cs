using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class RolController : Controller
    {
        private static readonly List<RolViewModel> _roles = new();
        private static readonly List<PermisoViewModel> _permisos = new()
        {
            new PermisoViewModel { Id = 1, Nombre = "Crear" },
            new PermisoViewModel { Id = 2, Nombre = "Editar" },
            new PermisoViewModel { Id = 3, Nombre = "Eliminar" }
        };
        private static int _nextId = 1;

        public IActionResult Index(int unidadId)
        {
            ViewBag.UnidadId = unidadId;
            return View(_roles);
        }

        [HttpGet]
        public JsonResult Listar(int unidadId)
        {
            var data = unidadId > 0
                ? _roles.Where(r => r.UnidadDeNegocioId == unidadId).ToList()
                : _roles.ToList();
            return Json(new { success = true, data });
        }

        [HttpGet]
        public JsonResult Obtener(int id)
        {
            var rol = _roles.FirstOrDefault(r => r.Id == id);
            if (rol == null)
                return Json(new { success = false, error = "No encontrado" });
            return Json(new { success = true, data = rol });
        }

        [HttpGet]
        public JsonResult ListarPermisos(int id)
        {
            var asignados = _roles.FirstOrDefault(r => r.Id == id)?.Permisos ?? new List<int>();
            var data = _permisos.Select(p => new
            {
                p.Id,
                p.Nombre,
                Asignado = asignados.Contains(p.Id)
            });
            return Json(new { success = true, data });
        }

        [HttpPost]
        public JsonResult Guardar([FromBody] RolViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.Id == 0)
            {
                model.Id = _nextId++;
                _roles.Add(model);
            }
            else
            {
                var existente = _roles.FirstOrDefault(r => r.Id == model.Id);
                if (existente == null)
                    return Json(new { success = false, error = "No se encontró el rol." });
                existente.Nombre = model.Nombre;
                existente.UnidadDeNegocioId = model.UnidadDeNegocioId;
                existente.Permisos = model.Permisos ?? new List<int>();
            }

            return Json(new { success = true, id = model.Id });
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var rol = _roles.FirstOrDefault(r => r.Id == id);
            if (rol != null)
                _roles.Remove(rol);
            return Json(new { success = true });
        }
    }
}
