using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Infrastructure.Models.Brands
{
    public class BrandRequest
    {
        public int Id { get; set; }        
        public int UnidadDeNegocio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Logotipo { get; set; } = null!;
    }
}
