using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.Menus;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class MenusApiClient : IMenuApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenusApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<MenuResponse>> GetMenusAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<MenuResponse>>("api/v1/Menus")
                   ?? Enumerable.Empty<MenuResponse>();
        }

        public async Task<IEnumerable<MenuResponse>> GetVisibleMenusAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<MenuResponse>>("api/v1/Menus/visible")
                   ?? Enumerable.Empty<MenuResponse>();
        }

        public async Task<IEnumerable<MenuResponse>> GetMenusByParentAsync(int? parentId)
        {
            AddBearerToken();
            var route = parentId.HasValue
                ? $"api/v1/Menus/parent/{parentId.Value}"
                : "api/v1/Menus/parent";

            return await _http.GetFromJsonAsync<IEnumerable<MenuResponse>>(route)
                   ?? Enumerable.Empty<MenuResponse>();
        }

        public async Task<PaginatedResponse<MenuResponse>> GetMenusByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Menus/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<MenuResponse>>(url)
                   ?? new PaginatedResponse<MenuResponse>();
        }

        public async Task<MenuResponse?> GetMenuByIdAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<MenuResponse>($"api/v1/Menus/{id}");
        }

        public async Task<int> CreateMenuAsync(MenuRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Menus", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateMenuAsync(UpdateMenuRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Menus", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Menus/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
