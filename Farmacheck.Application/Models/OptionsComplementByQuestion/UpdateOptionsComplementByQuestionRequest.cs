namespace Farmacheck.Application.Models.OptionsComplementByQuestion
{
    public class UpdateOptionsComplementByQuestionRequest
    {
        public string EtiquetaId { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? ListaDeOpcionesPredefinidas { get; set; } = null!;

        public bool? Estatus { get; set; }
    }
}
