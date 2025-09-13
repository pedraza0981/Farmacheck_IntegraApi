
namespace Farmacheck.Application.Models.MailingProgramacion
{
    public class TipoReporteRequest
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }
        
        public DateTime FechaCreacion { get; set; }

    }
}
