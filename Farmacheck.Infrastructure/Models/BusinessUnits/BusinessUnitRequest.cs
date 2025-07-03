namespace Farmacheck.Infrastructure.Models.BusinessUnits
{
    public class BusinessUnitRequest
    {
        public string Nombre { get; set; } = null!;
        public string? Rfc { get; set; }
        public string? Direccion { get; set; }
        public string? Logotipo { get; set; }
    }
}
