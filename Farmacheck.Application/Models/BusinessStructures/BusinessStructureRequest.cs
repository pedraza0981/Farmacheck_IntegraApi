namespace Farmacheck.Application.Models.BusinessStructures
{
    public class BusinessStructureRequest
    {
        public int ClienteId { get; set; }
        public int MarcaId { get; set; }
        public int? SubmarcaId { get; set; }
        public int ZonaId { get; set; }
    }
}
