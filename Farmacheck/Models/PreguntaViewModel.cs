using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Models
{
    public class PreguntaViewModel
    {
        public int Id { get; set; }

        public int SeccionDelCuestionarioId { get; set; }

        public int CuestionarioId { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public string? ImagenDeReferencia { get; set; }

        public string ArchivoImagen { get; set; } = null!;

        public int Secuencia { get; set; }

        public int TipoPreguntaId { get; set; }

        public bool PermiteMultipleSeleccion { get; set; }

        public bool? EsPreguntaObligatoria { get; set; }

        public bool? EsPreguntaConPonderacion { get; set; }

        public int? NumeroDeEvidenciasLimitadasA { get; set; }

        public int? NumeroDeEvidenciasObligatorias { get; set; }

        public bool? RequiereEvidencia { get; set; }

        public int? EtiquetaId { get; set; }

        public string? EtiquetaNombre { get; set; }

        public decimal? Ponderacion { get; set; }

        public bool? Estatus { get; set; }

        public FormatoDeRespuestaPorPreguntaViewModel FormatoDeRespuesta { get; set; } = null!;

        public List<OpcionesPorPreguntaViewModel>? OpcionesPorPregunta { get; set; }

        public List<EtiquetasPorEscalaNumericaViewModel>? EtiquetasPorEscalaNumerica { get; set; }
    }
}
