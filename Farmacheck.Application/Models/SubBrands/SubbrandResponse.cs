using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.SubBrands
{
    public class SubbrandResponse
    {
        public int Id { get; set; }

        public int Marca { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Estatus { get; set; }

        public DateTime? ModificadaEl { get; set; }
    }
}
