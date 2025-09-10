using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.PermissionsByRoles;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PermissionByRolesApiClient : IPermissionByRoleApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionByRolesApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddBearerToken()
        {
            if (_http.DefaultRequestHeaders.Authorization != null)
            {
                return;
            }

            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<PermissionByRoleResponse>> GetPermissionsByRolesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<PermissionByRoleResponse>>("api/v1/PermissionsByRoles")
                   ?? new List<PermissionByRoleResponse>();
        }

        public async Task<PaginatedResponse<PermissionByRoleResponse>> GetPermissionsByRolesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/PermissionsByRoles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<PermissionByRoleResponse>>(url)
                      ?? new PaginatedResponse<PermissionByRoleResponse>();

            return res;
        }

        public async Task<PermissionByRoleResponse?> GetPermissionByRoleAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<PermissionByRoleResponse>($"api/v1/PermissionsByRoles/{id}");
        }

        public async Task<List<PermissionByRoleResponse>> GetByRolAsync(int rolId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<PermissionByRoleResponse>>($"api/v1/PermissionsByRoles/rol/{rolId}")
                   ?? new List<PermissionByRoleResponse>();
        }

        public async Task<int> CreateAsync(PermissionByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/PermissionsByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdatePermissionByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/PermissionsByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/PermissionsByRoles/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
