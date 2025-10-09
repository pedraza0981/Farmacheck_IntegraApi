using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Menus;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Farmacheck.Controllers
{
    public class AsignacionMenuRolController : Controller
    {
        private readonly IRoleApiClient _roleApiClient;
        private readonly IMenuApiClient _menuApiClient;

        public AsignacionMenuRolController(IRoleApiClient roleApiClient, IMenuApiClient menuApiClient)
        {
            _roleApiClient = roleApiClient;
            _menuApiClient = menuApiClient;
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
    }
}
