namespace Farmacheck.Application.DTOs
{
    public class AssignedClientsByUserRoleDto
    {
        public int UserRoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string BusinessUnitName { get; set; } = null!;
        public int TotalClients { get; set; }
    }
}
