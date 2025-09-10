using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Roles;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class RolesApiClient : IRoleApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolesApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<RoleResponse>>("api/v1/Roles/all")
                   ?? Enumerable.Empty<RoleResponse>();
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesByBusinessUnitAsync(byte businessUnitId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<RoleResponse>>($"api/v1/Roles/businessunit/{businessUnitId}")
                   ?? Enumerable.Empty<RoleResponse>();
        }

        public async Task<List<RoleResponse>> GetRolesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<RoleResponse>>("api/v1/Roles")
                   ?? new List<RoleResponse>();
        }

        public async Task<PaginatedResponse<RoleResponse>> GetRolesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Roles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<RoleResponse>>(url)
                      ?? new PaginatedResponse<RoleResponse>();

            return res;
        }

        public async Task<RoleResponse?> GetRoleAsync(byte id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<RoleResponse>($"api/v1/Roles/{id}");
        }

        public async Task<RoleResponse?> GetRoleByNameAsync(string rolName)
        {
            try {
                AddBearerToken();
                return await _http.GetFromJsonAsync<RoleResponse>($"api/v1/Roles/name/{rolName}");
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<int> CreateAsync(RoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Roles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Roles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(byte id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Roles/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Roles/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

    }
}
