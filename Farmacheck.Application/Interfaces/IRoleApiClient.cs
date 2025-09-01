using Farmacheck.Application.Models.Roles;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IRoleApiClient
    {
        Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
        Task<IEnumerable<RoleResponse>> GetRolesByBusinessUnitAsync(byte businessUnitId);
        Task<List<RoleResponse>> GetRolesAsync();
        Task<PaginatedResponse<RoleResponse>> GetRolesByPageAsync(int page, int items);
        Task<RoleResponse?> GetRoleAsync(byte id);
        Task<RoleResponse?> GetRoleByNameAsync(string rolName);
        Task<int> CreateAsync(RoleRequest request);
        Task<bool> UpdateAsync(UpdateRoleRequest request);
        Task DeleteAsync(byte id);
        Task<string> GetReport();
    }
}
