using Farmacheck.Application.Models.HierarchyByRoles;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IHierarchyByRoleApiClient
    {
        Task<IEnumerable<HierarchyByRoleResponse>> GetAllHierarchyByRolesAsync();
        Task<List<HierarchyByRoleResponse>> GetAllAsync();
        Task<PaginatedResponse<HierarchyByRoleResponse>> GetByPageAsync(int page, int items);
        Task<HierarchyByRoleResponse?> GetAsync(int id);
        Task<int> CreateAsync(HierarchyByRoleRequest request);
        Task<bool> UpdateAsync(UpdateHierarchyByRoleRequest request);
        Task DeleteAsync(int id);
    }
}
