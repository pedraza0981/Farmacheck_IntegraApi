namespace Farmacheck.Application.Models.Sprints
{
    public class UpdateSprintRequest
    {
        public int Id { get; set; }

        public int UnidadDeNegocioId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime VigenciaDel { get; set; }

        public DateTime VigenciaAl { get; set; }

        public bool RequiereSupervision { get; set; }

        public int PeriodoDeSupervision { get; set; }

        public bool RequiereRevision { get; set; }

        public int PeriodoDeRevision { get; set; }

        public bool? Estatus { get; set; }
    }
}
