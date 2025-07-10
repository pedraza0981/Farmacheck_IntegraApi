namespace Farmacheck.Application.DTOs
{
    public class BusinessStructureRequestDto
    {
        public int ClienteId { get; set; }
        public int MarcaId { get; set; }
        public int? SubmarcaId { get; set; }
        public int ZonaId { get; set; }
        public bool? Estatus { get; set; }
    }
}
