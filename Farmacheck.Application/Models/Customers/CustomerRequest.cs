namespace Farmacheck.Application.Models.Customers
{
    public class CustomerRequest
    {
        public long Id { get; set; }
        public string CentroDeCosto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? NumeroDeTelefono { get; set; }
        public decimal LatitudGps { get; set; }
        public decimal LongitudGps { get; set; }
        public short RadioGps { get; set; }
        public short TipoDeClienteId { get; set; }
        public int MarcaId { get; set; }
        public int? SubmarcaId { get; set; }
        public int ZonaId { get; set; }
    }
}
