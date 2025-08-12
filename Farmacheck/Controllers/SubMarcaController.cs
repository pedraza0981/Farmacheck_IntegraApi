using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using System.Collections.Generic;
using System.Linq;
using Farmacheck.Application.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using Farmacheck.Application.Models.SubBrands;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Controllers
{
    public class SubMarcaController : Controller
    {
        private readonly ISubbrandApiClient _subbrandApi;
        private readonly IBrandApiClient _brandApi;
        private readonly IMapper _mapper;
        private const int _itemsPerPage = 5;

        public SubMarcaController(ISubbrandApiClient subbrandApi, IBrandApiClient brandApi, IMapper mapper)
        {
            _subbrandApi = subbrandApi;
            _brandApi = brandApi;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int marcaId, int page = 1)
        {
            ViewBag.MarcaId = marcaId;

            var apiData = await _subbrandApi.GetSubbrandsByPageAsync(page, _itemsPerPage);
            var dtos = _mapper.Map<List<SubmarcaDto>>(apiData.Items);
            var lista = _mapper.Map<List<SubMarca>>(dtos);

            var brands = await _brandApi.GetBrandsAsync();
            foreach (var s in lista)
            {
                var b = brands.FirstOrDefault(m => m.Id == s.MarcaId);
                s.MarcaNombre = b?.Nombre;
            }

            var result = new PaginatedResponse<SubMarca>
            {
                Items = lista,
                TotalCount = apiData.TotalCount,
                CurrentPage = apiData.CurrentPage,
                PageSize = apiData.PageSize,
                TotalPages = apiData.TotalPages,
                HasNextPage = apiData.HasNextPage,
                HasPreviousPage = apiData.HasPreviousPage
            };

            ViewBag.Page = page;
            ViewBag.HasMore = result.HasNextPage;

            return View(result);
        }

        [HttpGet]
        public async Task<JsonResult> Listar(int page = 1)
        {
            var apiData = await _subbrandApi.GetSubbrandsByPageAsync(page, _itemsPerPage);
            var dtos = _mapper.Map<List<SubmarcaDto>>(apiData.Items);
            var lista = _mapper.Map<List<SubMarca>>(dtos);

            var brands = await _brandApi.GetBrandsAsync();
            foreach (var s in lista)
            {
                var b = brands.FirstOrDefault(m => m.Id == s.MarcaId);
                s.MarcaNombre = b?.Nombre;
            }

            var result = new PaginatedResponse<SubMarca>
            {
                Items = lista,
                TotalCount = apiData.TotalCount,
                CurrentPage = apiData.CurrentPage,
                PageSize = apiData.PageSize,
                TotalPages = apiData.TotalPages,
                HasNextPage = apiData.HasNextPage,
                HasPreviousPage = apiData.HasPreviousPage
            };

            return Json(new { success = true, data = result });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPorMarca(int marcaId)
        {
            var apiData = await _subbrandApi.GetSubbrandsByBrandsAsync(new List<int> { marcaId });
            var dtos = _mapper.Map<List<SubmarcaDto>>(apiData);
            var submarcas = _mapper.Map<List<SubMarca>>(dtos);

            return Json(new { success = true, data = submarcas });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidadApi = await _subbrandApi.GetSubbrandAsync(id);
            if (entidadApi == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<SubmarcaDto>(entidadApi);
            var model = _mapper.Map<SubMarca>(dto);

            var marca = await _brandApi.GetBrandAsync(model.MarcaId);
            model.MarcaNombre = marca?.Nombre;

            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] SubMarca model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            model.Estatus ??= true;

            if (model.Id == 0)
            {
                var request = _mapper.Map<SubbrandRequest>(model);
                var id = await _subbrandApi.CreateAsync(request);
                model.Id = id;
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateSubbrandRequest>(model);
                var updated = await _subbrandApi.UpdateAsync(updateRequest);
                if (!updated)
                    return Json(new { success = false, error = "No se pudo actualizar" });
            }

            var marca = await _brandApi.GetBrandAsync(model.MarcaId);
            model.MarcaNombre = marca?.Nombre;

            return Json(new { success = true, id = model.Id });
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _subbrandApi.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
