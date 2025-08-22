using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class UsersByRolesApiClient : IUserByRoleApiClient
    {
        private readonly HttpClient _http;

        public UsersByRolesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserByRoleResponse>> GetUsersByRolesAsync()
        {
            return await _http.GetFromJsonAsync<List<UserByRoleResponse>>("api/v1/UsersByRoles")
                   ?? new List<UserByRoleResponse>();
        }

        public async Task<PaginatedResponse<UserByRoleResponse>> GetUsersByRolesByPageAsync(int page, int items)
        {
            var url = $"api/v1/UsersByRoles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<UserByRoleResponse>>(url)
                      ?? new PaginatedResponse<UserByRoleResponse>();

            return res;
        }

        public async Task<UserByRoleResponse?> GetUserByRoleAsync(int id)
        {
            return await _http.GetFromJsonAsync<UserByRoleResponse>($"api/v1/UsersByRoles/{id}");
        }

        public async Task<List<RelUserByRoleResponse>> GetByUserAsync(int usuarioId)
        {
            return await _http.GetFromJsonAsync<List<RelUserByRoleResponse>>($"api/v1/UsersByRoles/user/{usuarioId}")
                   ?? new List<RelUserByRoleResponse>();
        }

        public async Task<int> CreateAsync(UserByRoleRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/UsersByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateUserByRoleRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/UsersByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/UsersByRoles/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<string> GetReport()
        {
            var response = await _http.GetAsync("api/v1/UsersByRoles/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
