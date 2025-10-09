using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Menus;
using Farmacheck.Application.Models.RolMenus;
using Farmacheck.Models;
using Farmacheck.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Farmacheck.Controllers
{
    public class AsignacionMenuRolController : Controller
    {
        private readonly IRoleApiClient _roleApiClient;
        private readonly IMenuApiClient _menuApiClient;
        private readonly IRolMenuApiClient _rolMenuApiClient;

        public AsignacionMenuRolController(
            IRoleApiClient roleApiClient,
            IMenuApiClient menuApiClient,
            IRolMenuApiClient rolMenuApiClient)
        {
            _roleApiClient = roleApiClient;
            _menuApiClient = menuApiClient;
            _rolMenuApiClient = rolMenuApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AsignacionMenuRolViewModel();

            var roles = await _roleApiClient.GetRolesAsync();
            viewModel.Roles = roles?
                .OrderBy(r => r.Nombre)
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Nombre
                })
                .ToList() ?? new List<SelectListItem>();

            var menus = await _menuApiClient.GetMenusAsync();
            viewModel.MenuTree = BuildTree(menus?.ToList() ?? new List<MenuResponse>());

            return View(viewModel);
        }

        private static List<MenuTreeNode> BuildTree(List<MenuResponse> menus, int? parentId = null)
        {
            return menus
                .Where(menu => menu.ParentId == parentId)
                .OrderBy(menu => menu.Orden)
                .ThenBy(menu => menu.Nombre)
                .Select(menu => new MenuTreeNode
                {
                    Id = menu.Id,
                    Nombre = menu.Nombre,
                    Hijos = BuildTree(menus, menu.Id)
                })
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> GetMenusByRole(int roleId)
        {
            var rolMenus = await _rolMenuApiClient.GetRolMenusByRolAsync(roleId);

            if (rolMenus == null)
            {
                return Json(Enumerable.Empty<object>());
            }

            return Json(rolMenus);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarAsignaciones([FromBody] SaveMenuRoleAssignmentRequest request)
        {
            if (request == null || request.RoleId <= 0)
            {
                return Json(new { success = false, message = "Debe seleccionar un rol válido." });
            }

            try
            {
                var existingRolMenus = await _rolMenuApiClient.GetRolMenusByRolAsync(request.RoleId);
                var existingMenuIds = existingRolMenus?
                    .Where(menu => menu != null && menu.PuedeVer)
                    .Select(menu => menu.MenuId)
                    .ToHashSet() ?? new HashSet<int>();

                var desiredMenuIds = (request.MenuIds ?? new List<int>())
                    .Where(id => id > 0)
                    .ToHashSet();

                var menuIdsToAdd = desiredMenuIds.Except(existingMenuIds).ToList();
                var menuIdsToRemove = existingMenuIds.Except(desiredMenuIds).ToList();

                var creationTasks = menuIdsToAdd
                    .Select(menuId => _rolMenuApiClient.CreateRolMenuAsync(new RolMenuRequest
                    {
                        RolId = request.RoleId,
                        MenuId = menuId,
                        PuedeVer = true
                    }));

                var deletionTasks = menuIdsToRemove
                    .Select(menuId => _rolMenuApiClient.DeleteRolMenuAsync(request.RoleId, menuId));

                await Task.WhenAll(creationTasks);
                await Task.WhenAll(deletionTasks);

                var message = "Asignación actualizada correctamente.";
                if (menuIdsToAdd.Any() || menuIdsToRemove.Any())
                {
                    message = $"Asignación actualizada. Menús agregados: {menuIdsToAdd.Count}. Menús eliminados: {menuIdsToRemove.Count}.";
                }

                return Json(new
                {
                    success = true,
                    message,
                    added = menuIdsToAdd,
                    removed = menuIdsToRemove
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "No se pudo guardar la asignación de menús.",
                    error = ex.Message
                });
            }
        }
    }
}
