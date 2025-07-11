using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.SubBrands
{
    public class SubbrandRequest
    {
        public int MarcaId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
