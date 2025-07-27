using Farmacheck.Application.Models.HierarchyByRoles;

namespace Farmacheck.Application.Interfaces
{
    public interface IHierarchyByRoleApiClient
    {
        Task<List<HierarchyByRoleResponse>> GetAllAsync();
        Task<List<HierarchyByRoleResponse>> GetByPageAsync(int page, int items);
        Task<HierarchyByRoleResponse?> GetAsync(int id);
        Task<int> CreateAsync(HierarchyByRoleRequest request);
        Task<bool> UpdateAsync(UpdateHierarchyByRoleRequest request);
        Task DeleteAsync(int id);
    }
}
