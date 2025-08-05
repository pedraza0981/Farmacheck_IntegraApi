namespace Farmacheck.Application.Models.PermissionsByRoles
{
    public class PermissionByRoleResponse
    {
        public int Id { get; set; }
        public int PermisoId { get; set; }
        public int RolId { get; set; }
        public bool Estatus { get; set; }
        public DateTime CreadoEl { get; set; }
        public DateTime? ModificadoEl { get; set; }
    }
}
