using System;

namespace Farmacheck.Infrastructure.Models.BusinessUnits
{
    public class BusinessUnitResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Rfc { get; set; }
        public string? Direccion { get; set; }
        public string? Logotipo { get; set; }
        public DateTime? ModificadaEl { get; set; }
        public bool? Estatus { get; set; }
    }
}
