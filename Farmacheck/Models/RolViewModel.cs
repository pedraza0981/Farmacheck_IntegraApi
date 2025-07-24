namespace Farmacheck.Models
{
    public class RolViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool Estatus { get; set; }

        public int UnidadDeNegocioId { get; set; }

        public string UnidadDeNegocioNombre { get; set; }

        public List<int> Permisos { get; set; } = new List<int>();
    }
}
