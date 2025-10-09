using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.RolMenus;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacheck.Controllers
{
    public class RolMenusController : Controller
    {
        private readonly IRolMenuApiClient _rolMenuApiClient;
        private readonly IMapper _mapper;

        public RolMenusController(IRolMenuApiClient rolMenuApiClient, IMapper mapper)
        {
            _rolMenuApiClient = rolMenuApiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusAsync();
            var dtos = _mapper.Map<List<RolMenuDto>>(rolMenus);
            var viewModels = _mapper.Map<List<RolMenuViewModel>>(dtos)
                .OrderBy(r => r.RolId)
                .ThenBy(r => r.MenuId)
                .ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusAsync();
            var dtos = _mapper.Map<List<RolMenuDto>>(rolMenus);
            var viewModels = _mapper.Map<List<RolMenuViewModel>>(dtos)
                .OrderBy(r => r.RolId)
                .ThenBy(r => r.MenuId)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPaginas(int page, int items)
        {
            var pagedResult = await _rolMenuApiClient.GetRolMenusByPageAsync(page, items);
            var dtos = _mapper.Map<List<RolMenuDto>>(pagedResult.Items.ToList());
            var viewModels = _mapper.Map<List<RolMenuViewModel>>(dtos)
                .OrderBy(r => r.RolId)
                .ThenBy(r => r.MenuId)
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
            var rolMenu = await _rolMenuApiClient.GetRolMenuByIdAsync(id);
            if (rolMenu == null)
            {
                return Json(new { success = false, error = "No encontrado" });
            }

            var dto = _mapper.Map<RolMenuDto>(rolMenu);
            var viewModel = _mapper.Map<RolMenuViewModel>(dto);

            return Json(new { success = true, data = viewModel });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPorRol(int rolId)
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusByRolAsync(rolId);
            var dtos = _mapper.Map<List<RolMenuDto>>(rolMenus);
            var viewModels = _mapper.Map<List<RolMenuViewModel>>(dtos)
                .OrderBy(r => r.MenuId)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPorUsuario(int usuarioId)
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusByUsuarioAsync(usuarioId);
            var dtos = _mapper.Map<List<RolMenuUsuarioDto>>(rolMenus);
            var viewModels = _mapper.Map<List<RolMenuUsuarioViewModel>>(dtos)
                .OrderBy(r => r.Orden)
                .ToList();

            return Json(new { success = true, data = viewModels });
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] RolMenuViewModel model)
        {
            if (model.RolId <= 0 || model.MenuId <= 0)
            {
                return Json(new { success = false, error = "Rol y menú son obligatorios." });
            }

            if (model.Id == 0)
            {
                var request = _mapper.Map<RolMenuRequest>(model);
                var id = await _rolMenuApiClient.CreateRolMenuAsync(request);
                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateRolMenuRequest>(model);
                var updated = await _rolMenuApiClient.UpdateRolMenuAsync(updateRequest);
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
                var deleted = await _rolMenuApiClient.DeleteRolMenuAsync(id);
                return Json(new { success = deleted });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
