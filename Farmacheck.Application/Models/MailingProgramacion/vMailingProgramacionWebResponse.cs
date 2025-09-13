
namespace Farmacheck.Application.Models.MailingProgramacion
{
    public class vMailingProgramacionWebResponse
    {
        public long ProgramacionId { get; set; }

        public int TipoReporteId { get; set; }

        public string TipoReporteNombre { get; set; }

        public int PeriodicidadId { get; set; }

        public string PeriodicidadClave { get; set; }

        public string PeriodicidadDescripcion { get; set; }

        public TimeOnly HoraEnvio { get; set; }

        public string ZonaHorariaIana { get; set; }

        public int UnidadDeNegocioId { get; set; }

        public string UnidadDeNegocioNombre { get; set; }

        public int CuestionarioId { get; set; }

        public string CuestionarioNombre { get; set; }

        public int RolId { get; set; }

        public string RolNombre { get; set; }

        public string CronExpresion { get; set; }

        public byte? DiaSemana { get; set; }

        public string DiaSemanaNombre { get; set; }

        public byte? DiaMes { get; set; }

        public bool Activo { get; set; }

    }
}
