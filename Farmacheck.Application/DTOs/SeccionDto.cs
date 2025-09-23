namespace Farmacheck.Application.DTOs
{
    public class SeccionDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int? CategoriaId { get; set; }

        public string CategoriaNombre { get; set; } = null!;

        public bool Estatus { get; set; }
    }
}
