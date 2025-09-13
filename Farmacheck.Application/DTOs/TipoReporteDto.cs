using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.DTOs
{
    public class TipoReporteDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; private set; } = DateTime.Now;
    }
}
