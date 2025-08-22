using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IUserByRoleApiClient
    {
        Task<List<UserByRoleResponse>> GetUsersByRolesAsync();
        Task<PaginatedResponse<UserByRoleResponse>> GetUsersByRolesByPageAsync(int page, int items);
        Task<UserByRoleResponse?> GetUserByRoleAsync(int id);
        Task<List<RelUserByRoleResponse>> GetByUserAsync(int usuarioId);
        Task<int> CreateAsync(UserByRoleRequest request);
        Task<bool> UpdateAsync(UpdateUserByRoleRequest request);
        Task<bool> DeleteAsync(int id);
        Task<string> GetReport();
    }
}
