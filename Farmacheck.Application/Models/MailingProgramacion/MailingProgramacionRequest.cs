
using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.MailingProgramacion
{
    public class MailingProgramacionRequest
    {
        [Required]
        public int TipoReporte_id { get; set; }

        [Required]
        public int Periodicidad_id { get; set; }

        [Required, RegularExpression(@"^\d{2}:\d{2}$")] // "HH:mm"
        public string HoraEnvio { get; set; } = default!;

        [Required]
        public int ZonaHoraria_id { get; set; }

        [Required]
        public int UnidadDeNegocio_id { get; set; }

        [Required]
        public int Cuestionario_id { get; set; }
        public int Rol_id { get; set; }

        public byte? DiaSemana { get; set; }
        public byte? DiaMes { get; set; }

        public string? CronExpresion { get; set; } = null;

        public List<int>? Destinatarios { get; set; } = new();
    }
}
