using System;
namespace Farmacheck.Application.Models.CustomersRolesUsers
{
    public class CustomerRolUserResponse
    {
        public int Id { get; set; }
        public int RolPorUsuarioId { get; set; }
        public long ClienteId { get; set; }
        public DateTime AsignadoEl { get; set; }
        public bool Estatus { get; set; }
        public bool GeolocalizacionActiva { get; set; }
    }
}
