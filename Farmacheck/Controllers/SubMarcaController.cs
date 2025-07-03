using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;
using Farmacheck.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class SubMarcaController : Controller
    {
        private static readonly List<SubMarca> _submarcas = new();
        private static int _nextId = 1;
        private readonly IBrandApiClient _apiClient;

        public SubMarcaController(IBrandApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Index(int marcaId)
        {
            ViewBag.MarcaId = marcaId;
            var lista = _submarcas.Where(s => s.MarcaId == marcaId).ToList();
            return View(lista);
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int marcaId)
        {
            var lista = _submarcas.Where(s => s.MarcaId == marcaId).ToList();

            var brands = await _apiClient.GetBrandsAsync();
            foreach (var s in lista)
            {
                var b = brands.FirstOrDefault(m => m.Id == s.MarcaId);
                s.MarcaNombre = b?.Nombre;
            }

            return Json(new { success = true, data = lista });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = _submarcas.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var marca = await _apiClient.GetBrandAsync(entidad.MarcaId);
            entidad.MarcaNombre = marca?.Nombre;

            return Json(new { success = true, data = entidad });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] SubMarca model)
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

            var marca = await _apiClient.GetBrandAsync(model.MarcaId);
            model.MarcaNombre = marca?.Nombre;

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
