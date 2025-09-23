namespace Farmacheck.Application.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Estatus { get; set; }
    }
}
