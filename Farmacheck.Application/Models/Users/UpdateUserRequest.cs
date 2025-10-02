using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Farmacheck.Application.Models.Users
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public long? NumeroDeTelefono { get; set; }
        public bool Estatus { get; set; }
        [JsonPropertyName("actualizaPass")]
        public bool ActualizarPass { get; set; }
        public bool GeolocalizacionActiva { get; set; }
    }
}
