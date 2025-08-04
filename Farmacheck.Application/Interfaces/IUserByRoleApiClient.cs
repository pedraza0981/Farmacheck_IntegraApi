using Farmacheck.Application.Models.Users;

namespace Farmacheck.Application.Interfaces
{
    public interface IUserByRoleApiClient
    {
        Task<List<UserByRoleResponse>> GetUsersByRolesAsync();
        Task<List<UserByRoleResponse>> GetUsersByRolesByPageAsync(int page, int items);
        Task<UserByRoleResponse?> GetUserByRoleAsync(int id);
        Task<List<UserByRoleResponse>> GetByUserAsync(int usuarioId);
        Task<int> CreateAsync(UserByRoleRequest request);
        Task<bool> UpdateAsync(UpdateUserByRoleRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
