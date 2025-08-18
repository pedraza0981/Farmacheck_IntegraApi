using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Roles;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class RolesApiClient : IRoleApiClient
    {
        private readonly HttpClient _http;

        public RolesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<RoleResponse>>("api/v1/Roles/all")
                   ?? Enumerable.Empty<RoleResponse>();
        }

        public async Task<List<RoleResponse>> GetRolesAsync()
        {
            return await _http.GetFromJsonAsync<List<RoleResponse>>("api/v1/Roles")
                   ?? new List<RoleResponse>();
        }

        public async Task<PaginatedResponse<RoleResponse>> GetRolesByPageAsync(int page, int items)
        {
            var url = $"api/v1/Roles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<RoleResponse>>(url)
                      ?? new PaginatedResponse<RoleResponse>();

            return res;
        }

        public async Task<RoleResponse?> GetRoleAsync(byte id)
        {
            return await _http.GetFromJsonAsync<RoleResponse>($"api/v1/Roles/{id}");
        }

        public async Task<RoleResponse?> GetRoleByNameAsync(string rolName)
        {
            return await _http.GetFromJsonAsync<RoleResponse>($"api/v1/Roles/name/{rolName}");
        }

        public async Task<int> CreateAsync(RoleRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Roles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateRoleRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Roles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(byte id)
        {
            var response = await _http.DeleteAsync($"api/v1/Roles/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
