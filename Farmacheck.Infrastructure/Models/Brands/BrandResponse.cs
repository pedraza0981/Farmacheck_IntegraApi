using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Infrastructure.Models.Brands
{
    public class BrandResponse
    {
        public int Id { get; set; }        
        public int UnidadDeNegocio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Logotipo { get; set; } = null!;
        public DateTime? ModificadaEl { get; set; }
        public bool? Estatus { get; set; }
    }
}
