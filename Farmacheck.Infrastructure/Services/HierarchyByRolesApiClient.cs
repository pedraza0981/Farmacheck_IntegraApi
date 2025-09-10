using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.HierarchyByRoles;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class HierarchyByRolesApiClient : IHierarchyByRoleApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HierarchyByRolesApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<HierarchyByRoleResponse>> GetAllHierarchyByRolesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<HierarchyByRoleResponse>>("api/v1/HierarchyByRole/all")
                   ?? Enumerable.Empty<HierarchyByRoleResponse>();
        }

        public async Task<List<HierarchyByRoleResponse>> GetAllAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<HierarchyByRoleResponse>>("api/v1/HierarchyByRole")
                   ?? new List<HierarchyByRoleResponse>();
        }

        public async Task<PaginatedResponse<HierarchyByRoleResponse>> GetByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/HierarchyByRole/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<HierarchyByRoleResponse>>(url)
                      ?? new PaginatedResponse<HierarchyByRoleResponse>();

            return res;
        }

        public async Task<HierarchyByRoleResponse?> GetAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<HierarchyByRoleResponse>($"api/v1/HierarchyByRole/{id}");
        }

        public async Task<int> CreateAsync(HierarchyByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/HierarchyByRole", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateHierarchyByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/HierarchyByRole", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/HierarchyByRole/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
