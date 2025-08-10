using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.HierarchyByRoles;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class HierarchyByRolesApiClient : IHierarchyByRoleApiClient
    {
        private readonly HttpClient _http;

        public HierarchyByRolesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<HierarchyByRoleResponse>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<HierarchyByRoleResponse>>("api/v1/HierarchyByRole")
                   ?? new List<HierarchyByRoleResponse>();
        }

        public async Task<PaginatedResponse<HierarchyByRoleResponse>> GetByPageAsync(int page, int items)
        {
            var url = $"api/v1/HierarchyByRole/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<HierarchyByRoleResponse>>(url)
                   ?? new PaginatedResponse<HierarchyByRoleResponse>();
        }

        public async Task<HierarchyByRoleResponse?> GetAsync(int id)
        {
            return await _http.GetFromJsonAsync<HierarchyByRoleResponse>($"api/v1/HierarchyByRole/{id}");
        }

        public async Task<int> CreateAsync(HierarchyByRoleRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/HierarchyByRole", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateHierarchyByRoleRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/HierarchyByRole", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/HierarchyByRole/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
