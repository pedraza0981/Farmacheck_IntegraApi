using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class ClienteController : Controller
    {
        private static readonly List<ClienteEstructuraViewModel> _clientes = new();
        private static int _nextId = 1;

        private static readonly List<SelectListItem> _tiposCliente = new()
        {
            new SelectListItem { Value = "1", Text = "Tipo A" },
            new SelectListItem { Value = "2", Text = "Tipo B" }
        };

        private static readonly List<SelectListItem> _zonas = new()
        {
            new SelectListItem { Value = "1", Text = "Zona Norte" },
            new SelectListItem { Value = "2", Text = "Zona Sur" }
        };

        public IActionResult Index()
        {
            return View(_clientes);
        }

        [HttpGet]
        public JsonResult Listar()
        {
            return Json(new { success = true, data = _clientes });
        }

        [HttpGet]
        public JsonResult Obtener(int id)
        {
            var entidad = _clientes.FirstOrDefault(c => c.ClienteId == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });
            return Json(new { success = true, data = entidad });
        }

        [HttpPost]
        public JsonResult Guardar([FromBody] ClienteEstructuraViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.ClienteId == 0)
            {
                model.ClienteId = _nextId++;
                _clientes.Add(model);
            }
            else
            {
                var existente = _clientes.FirstOrDefault(c => c.ClienteId == model.ClienteId);
                if (existente == null)
                    return Json(new { success = false, error = "No encontrado" });

                existente.UnidadDeNegocioId = model.UnidadDeNegocioId;
                existente.CentroDeCosto = model.CentroDeCosto;
                existente.Nombre = model.Nombre;
                existente.Direccion = model.Direccion;
                existente.Estado = model.Estado;
                existente.NumeroDeTelefono = model.NumeroDeTelefono;
                existente.LatitudGPS = model.LatitudGPS;
                existente.LongitudGPS = model.LongitudGPS;
                existente.Estatus = model.Estatus;
                existente.RadioGPS = model.RadioGPS;
                existente.TipoDeClienteId = model.TipoDeClienteId;
                existente.MarcaId = model.MarcaId;
                existente.SubmarcaId = model.SubmarcaId;
                existente.ZonaId = model.ZonaId;
                existente.MarcaNombre = model.MarcaNombre;
                existente.SubmarcaNombre = model.SubmarcaNombre;
                existente.ZonaNombre = model.ZonaNombre;
                existente.ModificadoEl = System.DateTime.Now;
            }

            return Json(new { success = true, id = model.ClienteId });
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            var entidad = _clientes.FirstOrDefault(c => c.ClienteId == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });
            _clientes.Remove(entidad);
            return Json(new { success = true });
        }

        [HttpGet]
        public JsonResult ListarTiposCliente()
        {
            return Json(new { success = true, data = _tiposCliente });
        }

        [HttpGet]
        public JsonResult ListarZonas()
        {
            return Json(new { success = true, data = _zonas });
        }
    }
}
