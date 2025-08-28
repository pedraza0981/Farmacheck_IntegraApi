namespace Farmacheck.Application.Models.ResponseFormatByQuestion
{
    public class UpdateResponseFormatByQuestionRequest
    {
        public int FormatoId { get; set; }

        public bool PermiteMultipleSeleccion { get; set; }

        public bool? Estatus { get; set; }
    }
}
