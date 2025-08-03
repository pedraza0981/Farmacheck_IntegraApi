using Farmacheck.Application.Models.AssignedClientsByUserRole;

namespace Farmacheck.Application.Interfaces
{
    public interface IAssignedClientsByUserRoleApiClient
    {
        Task<AssignedClientsByUserRoleResponse?> GetByUserRoleAsync(int userRoleId);
    }
}
