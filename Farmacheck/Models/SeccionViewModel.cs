namespace Farmacheck.Models
{
    public class SeccionViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int? CategoriaId { get; set; }

        public string? CategoriaNombre { get; set; }
    }
}
