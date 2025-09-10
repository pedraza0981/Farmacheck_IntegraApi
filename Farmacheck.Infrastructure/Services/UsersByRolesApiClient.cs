using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class UsersByRolesApiClient : IUserByRoleApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersByRolesApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<UserByRoleResponse>> GetUsersByRolesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<UserByRoleResponse>>("api/v1/UsersByRoles")
                   ?? new List<UserByRoleResponse>();
        }

        public async Task<PaginatedResponse<UserByRoleResponse>> GetUsersByRolesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/UsersByRoles/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<UserByRoleResponse>>(url)
                      ?? new PaginatedResponse<UserByRoleResponse>();

            return res;
        }

        public async Task<UserByRoleResponse?> GetUserByRoleAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<UserByRoleResponse>($"api/v1/UsersByRoles/{id}");
        }

        public async Task<List<RelUserByRoleResponse>> GetByUserAsync(int usuarioId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<RelUserByRoleResponse>>($"api/v1/UsersByRoles/user/{usuarioId}")
                   ?? new List<RelUserByRoleResponse>();
        }

        public async Task<int> CreateAsync(UserByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/UsersByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateUserByRoleRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/UsersByRoles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/UsersByRoles/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/UsersByRoles/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
