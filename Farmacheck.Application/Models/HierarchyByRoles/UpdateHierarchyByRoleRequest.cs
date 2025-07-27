using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.HierarchyByRoles
{
    public class UpdateHierarchyByRoleRequest : HierarchyByRoleRequest
    {
        [Required]
        public int Id { get; set; }
        public bool? Estatus { get; set; }
    }
}
