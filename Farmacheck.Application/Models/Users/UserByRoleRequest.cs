namespace Farmacheck.Application.Models.Users
{
    public class UserByRoleRequest
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public int UnidadDeNegocioId { get; set; }
        public int AsignadoPor { get; set; }
        public IEnumerable<long> ClienteIds { get; set; } = new List<long>();
    }
}
