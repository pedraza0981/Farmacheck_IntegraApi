using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.Permissions
{
    public class UpdatePermissionRequest : PermissionRequest
    {
        [Required]
        public int Id { get; set; }
        public bool Estatus { get; set; }
    }
}
