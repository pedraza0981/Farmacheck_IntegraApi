using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.DTOs
{
    public class MarcaRequestDto : MarcaDto
    {
        public int Id { get; set; }

        public bool? Estatus { get; set; }
    }
}
