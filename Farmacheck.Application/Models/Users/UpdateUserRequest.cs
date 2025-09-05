using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.Users
{
    public class UpdateUserRequest : UserRequest
    {
        [Required]
        public int Id { get; set; }
        public bool Estatus { get; set; }
        public bool GeolocalizacionActiva { get; set; }
    }
}
