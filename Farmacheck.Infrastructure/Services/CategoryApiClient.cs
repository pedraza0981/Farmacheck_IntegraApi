using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Categories;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
        {
            AddBearerToken();
            var categories = await _http.GetFromJsonAsync<IEnumerable<CategoryResponse>>("api/v1/Categories")
                   ?? Enumerable.Empty<CategoryResponse>();

            return categories.OrderBy(c => c.Nombre);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            AddBearerToken();
            var categories = await _http.GetFromJsonAsync<IEnumerable<CategoryResponse>>("api/v1/Categories/all")
                   ?? Enumerable.Empty<CategoryResponse>();

            return categories.OrderBy(c => c.Nombre);
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetCategoriesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Categories/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<CategoryResponse>>(url)
                   ?? new PaginatedResponse<CategoryResponse>();
        }

        public async Task<CategoryResponse?> GetCategoryAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<CategoryResponse>($"api/v1/Categories/{id}");
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesByRoleAsync(int roleId)
        {
            AddBearerToken();
            var url = $"api/v1/Categories/role/{roleId}";
            var categories = await _http.GetFromJsonAsync<IEnumerable<CategoryResponse>>(url)
                   ?? Enumerable.Empty<CategoryResponse>();

            return categories.OrderBy(c => c.Nombre);
        }

        public async Task<int> CreateAsync(CategoryRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Categories", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateCategoryRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Categories", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Categories/{id}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<bool?>();

            return result ?? true;
        }

        public async Task<string> GetReportAsync()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Categories/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
