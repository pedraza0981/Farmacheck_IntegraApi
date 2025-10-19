using System;

namespace Farmacheck.Application.Models.BusinessUnits
{
    public class BusinessUnitResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Rfc { get; set; }
        public string? Direccion { get; set; }
        public string? Logotipo { get; set; }
        public string? ImagenDeReferencia { get; set; }
        public string? ArchivoImagen { get; set; }
        public DateTime? ModificadaEl { get; set; }
        public bool? Estatus { get; set; }
    }
}
