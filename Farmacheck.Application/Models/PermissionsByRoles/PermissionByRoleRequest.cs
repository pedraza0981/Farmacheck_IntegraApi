using Farmacheck.Application.Models.Permissions;

namespace Farmacheck.Application.Models.PermissionsByRoles
{
    public class PermissionByRoleRequest
    {
        public List<int> Permisos { get; set; } = new();
        public int RolId { get; set; }
    }
}
