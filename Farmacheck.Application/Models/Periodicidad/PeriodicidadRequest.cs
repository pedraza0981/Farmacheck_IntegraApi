using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Periodicidad
{
    public class PeriodicidadRequest
    {
        public int Id { get; set; }

        public string Clave { get; set; }

        public string Descripcion { get; set; }
    }
}
