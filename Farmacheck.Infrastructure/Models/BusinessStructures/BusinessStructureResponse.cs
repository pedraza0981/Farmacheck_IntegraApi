namespace Farmacheck.Infrastructure.Models.BusinessStructures
{
    public class BusinessStructureResponse
    {
        public long ClienteId { get; set; }
        public int MarcaId { get; set; }
        public int? SubmarcaId { get; set; }
        public int ZonaId { get; set; }
        public bool? Estatus { get; set; }
        public DateTime? ModificadaEl { get; set; }
    }
}
