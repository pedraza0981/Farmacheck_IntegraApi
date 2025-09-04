namespace Farmacheck.Application.Models.ResponseFormat
{
    public class ResponseFormatCatResponse
    {
        public int Id { get; set; }

        public string Formato { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool BasadoEnEscalaNumerica { get; set; }

        public bool Estatus { get; set; }
    }
}
