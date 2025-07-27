using Farmacheck.Application.Models.Permissions;

namespace Farmacheck.Application.Interfaces
{
    public interface IPermissionApiClient
    {
        Task<List<PermissionResponse>> GetPermissionsAsync();
        Task<List<PermissionResponse>> GetPermissionsByPageAsync(int page, int items);
        Task<PermissionResponse?> GetPermissionAsync(int id);
        Task<int> CreateAsync(PermissionRequest request);
        Task<bool> UpdateAsync(UpdatePermissionRequest request);
        Task DeleteAsync(int id);
    }
}
