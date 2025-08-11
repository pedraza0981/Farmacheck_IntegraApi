using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.PermissionsByRoles;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PermissionByRolesApiClient : IPermissionByRoleApiClient
    {
        private readonly HttpClient _http;

        public PermissionByRolesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PermissionByRoleResponse>> GetPermissionsByRolesAsync()
        {
            return await _http.GetFromJsonAsync<List<PermissionByRoleResponse>>("api/v1/PermissionsByRoles")
                   ?? new List<PermissionByRoleResponse>();
        }

        public async Task<PaginatedResponse<PermissionByRoleResponse>> GetPermissionsByRolesByPageAsync(int page, int items)
        {
            var url = $"api/v1/PermissionsByRoles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<PermissionByRoleResponse>>(url)
                      ?? new PaginatedResponse<PermissionByRoleResponse>();

            return res;
        }

        public async Task<PermissionByRoleResponse?> GetPermissionByRoleAsync(int id)
        {
            return await _http.GetFromJsonAsync<PermissionByRoleResponse>($"api/v1/PermissionsByRoles/{id}");
        }

        public async Task<List<PermissionByRoleResponse>> GetByRolAsync(int rolId)
        {
            return await _http.GetFromJsonAsync<List<PermissionByRoleResponse>>($"api/v1/PermissionsByRoles/rol/{rolId}")
                   ?? new List<PermissionByRoleResponse>();
        }

        public async Task<int> CreateAsync(PermissionByRoleRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/PermissionsByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdatePermissionByRoleRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/PermissionsByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/PermissionsByRoles/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
