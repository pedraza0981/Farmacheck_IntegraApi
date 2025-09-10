using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Permissions;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PermissionsApiClient : IPermissionApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionsApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<PermissionResponse>> GetPermissionsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<PermissionResponse>>("api/v1/Permissions")
                   ?? new List<PermissionResponse>();
        }

        public async Task<PaginatedResponse<PermissionResponse>> GetPermissionsByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Permissions/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<PermissionResponse>>(url)
                      ?? new PaginatedResponse<PermissionResponse>();

            return res;
        }

        public async Task<PermissionResponse?> GetPermissionAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<PermissionResponse>($"api/v1/Permissions/{id}");
        }

        public async Task<int> CreateAsync(PermissionRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Permissions", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdatePermissionRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Permissions", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Permissions/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
