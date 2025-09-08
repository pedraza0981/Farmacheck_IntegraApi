using System.Collections.Generic;

namespace Farmacheck.Application.Models.Users
{
    public class UserRequest
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public long? NumeroDeTelefono { get; set; }
        public List<int>? Roles { get; set; }
        public int AsignadoPor { get; set; }
        public bool GeolocalizacionActiva { get; set; }
    }
}
