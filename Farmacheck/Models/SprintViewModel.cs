namespace Farmacheck.Models
{
    public class SprintViewModel
    {
        public int Id { get; set; }

        public int UnidadDeNegocioId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime? VigenciaDel { get; set; }

        public DateTime? VigenciaAl { get; set; }

        public string? VigenciaInicio { get; set; } = string.Empty;

        public string? VigenciaFin { get; set; } = string.Empty;

        public bool RequiereSupervision { get; set; }

        public int? PeriodoDeSupervision { get; set; }

        public bool RequiereRevision { get; set; }

        public int? PeriodoDeRevision { get; set; }

        public bool? Estatus { get; set; }

        public List<SprintSupervisorViewModel>? Supervisores { get; set; }

        public List<SprintRevisorViewModel>? Revisores { get; set; }

        public List<TareaViewModel>? Tareas { get; set; }
    }
}
