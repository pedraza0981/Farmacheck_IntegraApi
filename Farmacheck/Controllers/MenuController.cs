using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Menus;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuApiClient _menuApiClient;
        private readonly IMapper _mapper;

        public MenuController(IMenuApiClient menuApiClient, IMapper mapper)
        {
            _menuApiClient = menuApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await _menuApiClient.GetMenusAsync();
            var dtos = _mapper.Map<List<MenuDto>>(menus);
            var viewModels = _mapper.Map<List<MenuViewModel>>(dtos)
                .OrderBy(m => m.Orden)
                .ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var menus = await _menuApiClient.GetMenusAsync();
            var dtos = _mapper.Map<List<MenuDto>>(menus);
            var viewModels = _mapper.Map<List<MenuViewModel>>(dtos)
                .OrderBy(m => m.Orden)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpGet]
        public async Task<JsonResult> ListarVisibles()
        {
            var menus = await _menuApiClient.GetVisibleMenusAsync();
            var dtos = _mapper.Map<List<MenuDto>>(menus);
            var viewModels = _mapper.Map<List<MenuViewModel>>(dtos)
                .OrderBy(m => m.Orden)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPorPadre(int? parentId)
        {
            var menus = await _menuApiClient.GetMenusByParentAsync(parentId);
            var dtos = _mapper.Map<List<MenuDto>>(menus);
            var viewModels = _mapper.Map<List<MenuViewModel>>(dtos)
                .OrderBy(m => m.Orden)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPaginas(int page, int items)
        {
            var pagedResult = await _menuApiClient.GetMenusByPageAsync(page, items);
            var dtos = _mapper.Map<List<MenuDto>>(pagedResult.Items.ToList());
            var viewModels = _mapper.Map<List<MenuViewModel>>(dtos)
                .OrderBy(m => m.Orden)
                .ToList();

            return Json(new
            {
                success = true,
                data = viewModels,
                total = pagedResult.TotalCount,
                currentPage = pagedResult.CurrentPage,
                pageSize = pagedResult.PageSize,
                totalPages = pagedResult.TotalPages,
                hasNextPage = pagedResult.HasNextPage,
                hasPreviousPage = pagedResult.HasPreviousPage
            });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var menu = await _menuApiClient.GetMenuByIdAsync(id);
            if (menu == null)
            {
                return Json(new { success = false, error = "No encontrado" });
            }

            var dto = _mapper.Map<MenuDto>(menu);
            var viewModel = _mapper.Map<MenuViewModel>(dto);

            return Json(new { success = true, data = viewModel });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] MenuViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                return Json(new { success = false, error = "El nombre es obligatorio." });
            }

            if (model.Id == 0)
            {
                var request = _mapper.Map<MenuRequest>(model);
                var id = await _menuApiClient.CreateMenuAsync(request);
                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateMenuRequest>(model);
                var updated = await _menuApiClient.UpdateMenuAsync(updateRequest);
                if (!updated)
                {
                    return Json(new { success = false, error = "No se pudo actualizar" });
                }

                return Json(new { success = true, id = model.Id });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            try
            {
                var deleted = await _menuApiClient.DeleteMenuAsync(id);
                return Json(new { success = deleted });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
