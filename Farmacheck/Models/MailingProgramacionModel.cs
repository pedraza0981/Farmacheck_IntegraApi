using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacheck.Models
{
    public class MailingProgramacionModel
    {
        public long Id { get; set; }

        public int TipoReporte_id { get; set; }

        public int Periodicidad_id { get; set; }

        public TimeOnly HoraEnvio { get; set; }

        public int ZonaHoraria_id { get; set; }

        public int UnidadDeNegocio_id { get; set; }

        public int Cuestionario_id { get; set; }

        public int Rol_id { get; set; }

        public byte? DiaSemana { get; set; }

        public byte? DiaMes { get; set; }

        public string CronExpresion { get; set; }

        public bool Activo { get; set; }

        public DateTime CreadoUtc { get; set; }

        public int? CreadoPorUsuario_id { get; set; }

        public DateTime? ModificadoUtc { get; set; }

        public int? ModificadoPorUsuario_id { get; set; }

    }
}
