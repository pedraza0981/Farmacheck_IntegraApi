namespace Farmacheck.Application.Models.LabelsByNumericalScale
{
    public class LabelsByNumericalScaleRequest
    {
        public int? LimiteInferior { get; set; }

        public int? LimiteSuperior { get; set; }

        public string? EtiquetaParaEscalaInferior { get; set; } = null!;

        public string? EtiquetaParaEscalaSuperior { get; set; } = null!;
    }
}
