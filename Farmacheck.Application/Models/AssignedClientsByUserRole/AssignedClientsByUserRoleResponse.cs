namespace Farmacheck.Application.Models.AssignedClientsByUserRole
{
    public class AssignedClientsByUserRoleResponse
    {
        public int UserRoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string BusinessUnitName { get; set; } = null!;
        public int TotalClients { get; set; }
    }
}
