using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.CustomersRolesUsers
{
    public class UpdateCustomerRolUserRequest : CustomerRolUserRequest
    {
        [Required]
        public int Id { get; set; }
        public bool Estatus { get; set; }
    }
}
