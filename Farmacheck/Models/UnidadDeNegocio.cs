using System.Collections.Generic;

namespace Farmacheck.Models
{
    public class UnidadDeNegocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<MarcaViewModel> Marcas { get; set; } = new List<MarcaViewModel>();
    }
}
