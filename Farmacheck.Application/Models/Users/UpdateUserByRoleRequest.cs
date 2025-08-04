using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.Users
{
    public class UpdateUserByRoleRequest : UserByRoleRequest
    {
        [Required]
        public int Id { get; set; }
        public bool Estatus { get; set; }
    }
}
