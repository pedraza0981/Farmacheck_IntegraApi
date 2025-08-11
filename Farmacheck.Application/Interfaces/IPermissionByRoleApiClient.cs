using Farmacheck.Application.Models.PermissionsByRoles;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IPermissionByRoleApiClient
    {
        Task<List<PermissionByRoleResponse>> GetPermissionsByRolesAsync();
        Task<PaginatedResponse<PermissionByRoleResponse>> GetPermissionsByRolesByPageAsync(int page, int items);
        Task<PermissionByRoleResponse?> GetPermissionByRoleAsync(int id);
        Task<List<PermissionByRoleResponse>> GetByRolAsync(int rolId);
        Task<int> CreateAsync(PermissionByRoleRequest request);
        Task<bool> UpdateAsync(UpdatePermissionByRoleRequest request);
        Task DeleteAsync(int id);
    }
}
