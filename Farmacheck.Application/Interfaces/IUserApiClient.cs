using Farmacheck.Application.Models.Users;

namespace Farmacheck.Application.Interfaces
{
    public interface IUserApiClient
    {
        Task<List<UserResponse>> GetUsersAsync();
        Task<List<UserResponse>> GetUsersByPageAsync(int page, int items);
        Task<UserResponse?> GetUserAsync(int id);
        Task<List<UserByRoleResponse>> GetRolesByUserAsync(int usuarioId);
        Task<int> CreateAsync(UserRequest request);
        Task<bool> UpdateAsync(UpdateUserRequest request);
        Task DeleteAsync(int id);
    }
}
