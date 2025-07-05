using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.DTOs
{
    public class MarcaDto
    {
        public int Id { get; set; }
        public int UnidadDeNegocioId { get; set; }
        public string Nombre { get; set; }
        public string Logotipo { get; set; }
                
    }
}
