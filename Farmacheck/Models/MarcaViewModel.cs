using System.Collections.Generic;

namespace Farmacheck.Models
{
    public class MarcaViewModel
    {
        public int Id { get; set; }
        public int UnidadDeNegocioId { get; set; }
        public string Nombre { get; set; }
        public string Logotipo { get; set; }

        public UnidadDeNegocio UnidadDeNegocio { get; set; }
        public ICollection<SubMarca> SubMarcas { get; set; } = new List<SubMarca>();
    }
}
