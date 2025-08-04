using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class UsersApiClient : IUserApiClient
    {
        private readonly HttpClient _http;

        public UsersApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserResponse>> GetUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserResponse>>("api/v1/Users")
                   ?? new List<UserResponse>();
        }

        public async Task<List<UserResponse>> GetUsersByPageAsync(int page, int items)
        {
            var url = $"api/v1/Users/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<UserResponse>>(url)
                   ?? new List<UserResponse>();
        }

        public async Task<UserResponse?> GetUserAsync(int id)
        {
            return await _http.GetFromJsonAsync<UserResponse>($"api/v1/Users/{id}");
        }

        public async Task<List<UserByRoleResponse>> GetRolesByUserAsync(int usuarioId)
        {
            return await _http.GetFromJsonAsync<List<UserByRoleResponse>>($"api/v1/Users/{usuarioId}/roles")
                   ?? new List<UserByRoleResponse>();
        }

        public async Task<int> CreateAsync(UserRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Users", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateUserRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Users", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Users/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
