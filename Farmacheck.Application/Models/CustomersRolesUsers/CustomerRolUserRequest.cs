using System.Collections.Generic;
namespace Farmacheck.Application.Models.CustomersRolesUsers
{
    public class CustomerRolUserRequest
    {
        public int RolPorUsuarioId { get; set; }
        public List<long> Clientes { get; set; } = new List<long>();
        public bool GeolocalizacionActiva { get; set; }
    }
}
