using Farmacheck.Application.Models.LabelsByNumericalScale;
using Farmacheck.Application.Models.OptionsByQuestion;
using Farmacheck.Application.Models.ResponseFormatByQuestion;

namespace Farmacheck.Application.Models.Questions
{
    public class QuestionRequest
    {
        public int PreguntaId { get; set; }

        public int SeccionId { get; set; }

        public int CuestionarioId { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int Secuencia { get; set; }

        public string? ImagenDeReferencia { get; set; }

        public bool EsPreguntaObligatoria { get; set; }

        public bool EsPreguntaConPonderacion { get; set; }

        public int NumeroDeEvidenciasLimitadasA { get; set; }

        public int NumeroDeEvidenciasObligatorias { get; set; }

        public bool RequiereEvidencia { get; set; }

        public bool? Estatus { get; set; }

        public ResponseFormatByQuestionRequest FormatoDeRespuesta { get; set; } = null!;

        public IEnumerable<OptionsByQuestionRequest>? OpcionesPorPregunta { get; set; }

        public IEnumerable<LabelsByNumericalScaleRequest>? EtiquetasPorEscalaNumerica { get; set; }
    }
}
