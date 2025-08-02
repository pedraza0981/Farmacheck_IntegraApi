using System;
using System.Collections.Generic;

namespace Farmacheck.Application.DTOs
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string CentroDeCosto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? NumeroDeTelefono { get; set; }
        public decimal LatitudGps { get; set; }
        public decimal LongitudGps { get; set; }
        public DateTime ModificadoEl { get; set; }
        public bool Estatus { get; set; }
        public short RadioGps { get; set; }
        public short TipoDeClienteId { get; set; }
        public IEnumerable<BusinessStructureDto> BusinessStructure { get; set; } = null!;
    }
}
