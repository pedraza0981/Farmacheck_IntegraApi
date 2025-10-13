using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class AddEventoCalendarRequest
    {
        [JsonPropertyName("calendarioId"), Required, Range(1, int.MaxValue)]
        public int CalendarioId { get; set; }

        [JsonPropertyName("titulo"), Required, MaxLength(200)]
        public string Titulo { get; set; } = default!;

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }

        [JsonPropertyName("ubicacion"), MaxLength(250)]
        public string? Ubicacion { get; set; }

        [JsonPropertyName("fechaInicioUtc"), Required]
        public DateTime FechaInicioUtc { get; set; }

        [JsonPropertyName("fechaFinUtc"), Required]
        public DateTime FechaFinUtc { get; set; }

        [JsonPropertyName("todoElDia")]
        public bool? TodoElDia { get; set; } = false;

        [JsonPropertyName("estatus")]
        [Range(0, byte.MaxValue)]
        public byte? Estatus { get; set; } = 1;

        [JsonPropertyName("visibilidad")]
        [Range(0, byte.MaxValue)]
        public byte? Visibilidad { get; set; } = 0; // 0:Privado, 1:Compartido

        [JsonPropertyName("colorHex")]
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "ColorHex debe ser como #RRGGBB")]
        public string? ColorHex { get; set; }

        [JsonPropertyName("zonaHorariaIana"), MaxLength(80)]
        public string? ZonaHorariaIana { get; set; }

        [JsonPropertyName("esRecurrente")]
        public bool? EsRecurrente { get; set; } = false;

        [JsonPropertyName("etiquetaId")]
        public int? EtiquetaId { get; set; }

        // IDs de usuarios que asistirán al evento (opcional, 1..n)
        [JsonPropertyName("asistentesUsuarioId")]
        public List<int>? AsistentesUsuarioId { get; set; }

        // IDs de archivos previamente cargados en dbo.Archivo (opcional, 0..n)
        [JsonPropertyName("archivosId")]
        public List<int>? ArchivosId { get; set; }
    }
}
