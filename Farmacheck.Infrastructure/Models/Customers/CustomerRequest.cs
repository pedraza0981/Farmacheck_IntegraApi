namespace Farmacheck.Infrastructure.Models.Customers
{
    public class CustomerRequest
    {
        public string CentroDeCosto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? NumeroDeTelefono { get; set; }
        public decimal LatitudGps { get; set; }
        public decimal LongitudGps { get; set; }
        public short RadioGps { get; set; }
        public short TipoDeClienteId { get; set; }
    }
}
