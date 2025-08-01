using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Brands
{
    public class BrandRequest
    {        
        public int UnidadDeNegocioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Logotipo { get; set; } = null!;
        public bool Estatus { get; set; }
    }
}
