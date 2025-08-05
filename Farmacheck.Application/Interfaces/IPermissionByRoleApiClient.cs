using Farmacheck.Application.Models.PermissionsByRoles;

namespace Farmacheck.Application.Interfaces
{
    public interface IPermissionByRoleApiClient
    {
        Task<List<PermissionByRoleResponse>> GetPermissionsByRolesAsync();
        Task<List<PermissionByRoleResponse>> GetPermissionsByRolesByPageAsync(int page, int items);
        Task<PermissionByRoleResponse?> GetPermissionByRoleAsync(int id);
        Task<List<PermissionByRoleResponse>> GetByRolAsync(int rolId);
        Task<int> CreateAsync(PermissionByRoleRequest request);
        Task<bool> UpdateAsync(UpdatePermissionByRoleRequest request);
        Task DeleteAsync(int id);
    }
}
