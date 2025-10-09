using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.RolMenus;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class RolMenusApiClient : IRolMenuApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolMenusApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<RolMenuResponse>> GetRolMenusAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<RolMenuResponse>>("api/v1/RolMenus")
                   ?? Enumerable.Empty<RolMenuResponse>();
        }

        public async Task<PaginatedResponse<RolMenuResponse>> GetRolMenusByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/RolMenus/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<RolMenuResponse>>(url)
                   ?? new PaginatedResponse<RolMenuResponse>();
        }

        public async Task<RolMenuResponse?> GetRolMenuByIdAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<RolMenuResponse>($"api/v1/RolMenus/{id}");
        }

        public async Task<IEnumerable<RolMenuResponse>> GetRolMenusByRolAsync(int rolId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<RolMenuResponse>>($"api/v1/RolMenus/rol/{rolId}")
                   ?? Enumerable.Empty<RolMenuResponse>();
        }

        public async Task<IEnumerable<RolMenuUsuarioResponse>> GetRolMenusByUsuarioAsync(int usuarioId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<RolMenuUsuarioResponse>>($"api/v1/RolMenus/usuario/{usuarioId}")
                   ?? Enumerable.Empty<RolMenuUsuarioResponse>();
        }

        public async Task<int> CreateRolMenuAsync(RolMenuRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/RolMenus", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateRolMenuAsync(UpdateRolMenuRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/RolMenus", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteRolMenuAsync(int rolId, int menuId)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/RolMenus/{rolId}/{menuId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
