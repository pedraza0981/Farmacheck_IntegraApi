namespace Farmacheck.Application.Models.OptionsComplementByQuestion
{
    public class OptionsComplementByQuestionRequest
    {
        public string EtiquetaId { get; set; } = null!;

        public string? Nombre { get; set; } = null!;

        public string? ListaDeOpcionesPredefinidas { get; set; } = null!;
    }
}
