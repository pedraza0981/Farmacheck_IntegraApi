using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.Roles
{
    public class UpdateRoleRequest : RoleRequest
    {
        [Required]
        public int Id { get; set; }
        public bool Estatus { get; set; }
    }
}
