using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Brands
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public int UnidadDeNegocio { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ImagenDeReferencia { get; set; }
        public string? ArchivoImagen { get; set; }
        public string? Logotipo { get; set; }
        public DateTime? ModificadaEl { get; set; }
        public bool? Estatus { get; set; }
    }
}
