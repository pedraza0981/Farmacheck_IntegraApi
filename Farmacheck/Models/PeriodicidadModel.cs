using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacheck.Models
{
    public class PeriodicidadModel
    {
        public int Id { get; set; }

        public string Clave { get; set; }

        public string Descripcion { get; set; }

    }
}
