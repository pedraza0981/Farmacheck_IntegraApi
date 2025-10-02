using System.Text.Json.Serialization;

namespace Farmacheck.Application.Models.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public long? NumeroDeTelefono { get; set; }
        public bool Estatus { get; set; }
        public DateTime CreadoEl { get; set; }
        public DateTime ActualizadoEl { get; set; }

        [JsonPropertyName("actualizaPass")]
        public bool ActualizarPass { get; set; }

        public bool GeolocalizacionActiva { get; set; }
    }
}
