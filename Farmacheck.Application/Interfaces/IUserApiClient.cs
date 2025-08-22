using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IUserApiClient
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<List<UserResponse>> GetUsersAsync();
        Task<PaginatedResponse<UserResponse>> GetUsersByPageAsync(int page, int items);
        Task<UserResponse?> GetUserAsync(int id);
        Task<int> CreateAsync(UserRequest request);
        Task<bool> UpdateAsync(UpdateUserRequest request);
        Task DeleteAsync(int id);
        Task<string> GetReport();
    }
}
