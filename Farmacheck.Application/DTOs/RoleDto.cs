namespace Farmacheck.Application.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estatus { get; set; }
        public int UnidadDeNegocioId { get; set; }
        public bool AccesoWeb { get; set; }
    }
}
