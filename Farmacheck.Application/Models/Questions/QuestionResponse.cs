using Farmacheck.Application.Models.LabelsByNumericalScale;
using Farmacheck.Application.Models.OptionsByQuestion;
using Farmacheck.Application.Models.ResponseFormatByQuestion;

namespace Farmacheck.Application.Models.Questions
{
    public class QuestionResponse
    {
        public int Id { get; set; }

        public int SeccionDelCuestionarioId { get; set; }

        public int CuestionarioId { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public string? ImagenDeReferencia { get; set; }

        public int Secuencia { get; set; }

        public bool? EsPreguntaObligatoria { get; set; }

        public bool? EsPreguntaConPonderacion { get; set; }

        public int NumeroDeEvidenciasLimitadasA { get; set; }

        public int NumeroDeEvidenciasObligatorias { get; set; }

        public bool? RequiereEvidencia { get; set; }

        public int? EtiquetaId { get; set; }

        public string? EtiquetaNombre { get; set; }

        public decimal? Ponderacion { get; set; }

        public bool? Estatus { get; set; }

        public ResponseFormatByQuestionResponse FormatoDeRespuesta { get; set; } = null!;

        public IEnumerable<OptionsByQuestionResponse>? OpcionesPorPregunta { get; set; }

        public IEnumerable<LabelsByNumericalScaleResponse>? EtiquetasPorEscalaNumerica { get; set; }
    }
}
