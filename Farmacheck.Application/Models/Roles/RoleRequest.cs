namespace Farmacheck.Application.Models.Roles
{
    public class RoleRequest
    {
        public string Nombre { get; set; } = null!;
        public int UnidadDeNegocioId { get; set; }
        public bool AccesoWeb { get; set; }
    }
}
