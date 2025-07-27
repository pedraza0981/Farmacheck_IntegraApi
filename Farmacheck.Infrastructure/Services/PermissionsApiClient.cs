using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Permissions;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PermissionsApiClient : IPermissionApiClient
    {
        private readonly HttpClient _http;

        public PermissionsApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PermissionResponse>> GetPermissionsAsync()
        {
            return await _http.GetFromJsonAsync<List<PermissionResponse>>("api/v1/Permissions")
                   ?? new List<PermissionResponse>();
        }

        public async Task<List<PermissionResponse>> GetPermissionsByPageAsync(int page, int items)
        {
            var url = $"api/v1/Permissions/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<PermissionResponse>>(url)
                   ?? new List<PermissionResponse>();
        }

        public async Task<PermissionResponse?> GetPermissionAsync(int id)
        {
            return await _http.GetFromJsonAsync<PermissionResponse>($"api/v1/Permissions/{id}");
        }

        public async Task<int> CreateAsync(PermissionRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Permissions", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdatePermissionRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Permissions", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Permissions/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
