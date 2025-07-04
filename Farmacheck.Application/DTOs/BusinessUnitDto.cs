using System;

namespace Farmacheck.Application.DTOs
{
    public class BusinessUnitDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Rfc { get; set; }
        public string? Direccion { get; set; }
        public string? Logotipo { get; set; }        
        public DateTime? ModificadaEl { get; set; }
        public bool? Estatus { get; set; }
    }
}
