using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Farmacheck.Models
{
    public class AsignacionMenuRolViewModel
    {
        public List<SelectListItem> Roles { get; set; } = new();

        public List<MenuTreeNode> MenuTree { get; set; } = new();
    }

    public class MenuTreeNode
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public bool Seleccionado { get; set; }

        public List<MenuTreeNode> Hijos { get; set; } = new();
    }
}
