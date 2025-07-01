using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Infrastructure.Services;
using AutoMapper;
using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.Brands;
using static System.Net.Mime.MediaTypeNames;
using Farmacheck.Application.DTOs;

namespace Farmacheck.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IBrandApiClient _apiClient; 
        private readonly IMapper _mapper;

        public MarcaController(IBrandApiClient apiClient, IMapper mapper)
        {         
            _apiClient = apiClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int unidadId)
        {
            ViewBag.UnidadId = unidadId;

            var apiData = await _apiClient.GetBrandsAsync(); 
            var marcaDtos = _mapper.Map<List<MarcaDto>>(apiData); 
            var marcas = _mapper.Map<List<MarcaViewModel>>(marcaDtos); 

            //var lista = marcas.Where(m => m.UnidadDeNegocioId == unidadId).ToList();

            return View(marcas);
        }

        [HttpGet]
        [HttpGet]
        public async Task<JsonResult> Listar(int unidadId)
        {
            var apiData = await _apiClient.GetBrandsAsync();
            var marcaDtos = _mapper.Map<List<MarcaDto>>(apiData);
            var marcas = _mapper.Map<List<MarcaViewModel>>(marcaDtos);

            //var lista = marcas.Where(m => m.UnidadDeNegocioId == unidadId).ToList();

            return Json(new { success = true, data = marcas });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetBrandAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var marca = _mapper.Map<MarcaViewModel>(entidad); 
            return Json(new { success = true, data = marca });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] MarcaViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            var request = _mapper.Map<BrandRequest>(model); 
            var id = await _apiClient.CreateAsync(request); 
            return Json(new { success = true, id });
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
