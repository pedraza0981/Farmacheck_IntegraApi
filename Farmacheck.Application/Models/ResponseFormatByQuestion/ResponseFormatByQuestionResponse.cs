namespace Farmacheck.Application.Models.ResponseFormatByQuestion
{
    public class ResponseFormatByQuestionResponse
    {
        public int FormatoId { get; set; }

        public string FormatoNombre { get; set; } = string.Empty;

        public bool PermiteMultipleSeleccion { get; set; }

        public bool Estatus { get; set; }
    }
}
