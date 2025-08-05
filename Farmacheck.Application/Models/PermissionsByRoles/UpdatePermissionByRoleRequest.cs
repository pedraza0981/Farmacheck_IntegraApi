using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.PermissionsByRoles
{
    public class UpdatePermissionByRoleRequest : PermissionByRoleRequest
    {
        [Required]
        public int Id { get; set; }
        public int PermisoId { get; set; }
        public bool Estatus { get; set; }
    }
}
