namespace Farmacheck.Application.Models.BusinessStructures
{
    public class BusinessStructureResponse
    {
        public long? ClienteId { get; set; }
        public string? Cliente { get; set; }
        public int? MarcaId { get; set; }
        public string? Marca { get; set; }
        public int? SubmarcaId { get; set; }
        public string? Submarca { get; set; }
        public int ZonaId { get; set; }
        public string? Zona { get; set; }
        public bool? Estatus { get; set; }
        public DateTime? ModificadaEl { get; set; }
    }
}
