
namespace Farmacheck.Application.Models.MailingProgramacion
{
    public class MailingProgramacionDestinatarioPostResponse
    {
        public int TipoReporte_id { get; set; }

        public int Periodicidad_id { get; set; }

        public TimeOnly HoraEnvio { get; set; }

        public int ZonaHoraria_id { get; set; }

        public int UnidadDeNegocio_id { get; set; }

        public int Cuestionario_id { get; set; }

        public int Rol_id { get; set; }

        public byte? DiaSemana { get; set; }

        public byte? DiaMes { get; set; }

        public string? CronExpresion { get; set; }

        public List<MailingProgramacionDestinatarioResponse>? Destinatarios { get; set; }
    }
}
