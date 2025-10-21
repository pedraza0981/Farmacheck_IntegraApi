namespace Farmacheck.Application.Models.Roles
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estatus { get; set; }
        public int UnidadDeNegocioId { get; set; }
        public bool AccesoWeb { get; set; }
    }
}
