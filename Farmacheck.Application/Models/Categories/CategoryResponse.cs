namespace Farmacheck.Application.Models.Categories
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Estatus { get; set; }
    }
}
