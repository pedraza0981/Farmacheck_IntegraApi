namespace Farmacheck.Application.Models.HierarchyByRoles
{
    public class HierarchyByRoleRequest
    {
        public string Nombre { get; set; } = null!;
        public int RolSuperiorId { get; set; }
        public int RolSubordinadoId { get; set; }

        public bool Estatus { get; set; }
    }
}
