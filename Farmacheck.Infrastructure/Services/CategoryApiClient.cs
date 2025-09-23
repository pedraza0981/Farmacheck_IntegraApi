using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Categories;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;

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
            var response = await GetAsync<IEnumerable<CategoryResponse>>("api/v1/Categories");

            return (response ?? Enumerable.Empty<CategoryResponse>()).OrderBy(c => c.Nombre);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var response = await GetAsync<IEnumerable<CategoryResponse>>("api/v1/Categories/all");

            return (response ?? Enumerable.Empty<CategoryResponse>()).OrderBy(c => c.Nombre);
        }

        public async Task<PaginatedResponse<CategoryResponse>> GetCategoriesByPageAsync(int page, int items)
        {
            var url = $"api/v1/Categories/pages?page={page}&items={items}";
            return await GetAsync<PaginatedResponse<CategoryResponse>>(url)
                   ?? new PaginatedResponse<CategoryResponse>();
        }

        public async Task<CategoryResponse?> GetCategoryAsync(int id)
        {
            return await GetAsync<CategoryResponse>($"api/v1/Categories/{id}");
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesByRoleAsync(int roleId)
        {
            var url = $"api/v1/Categories/role/{roleId}";
            var response = await GetAsync<IEnumerable<CategoryResponse>>(url);

            return (response ?? Enumerable.Empty<CategoryResponse>()).OrderBy(c => c.Nombre);
        }

        public async Task<int> CreateAsync(CategoryRequest request)
        {
            var response = await SendAsync<int>(() => _http.PostAsJsonAsync("api/v1/Categories", request));
            return response ?? 0;
        }

        public async Task<bool> UpdateAsync(UpdateCategoryRequest request)
        {
            var response = await SendAsync<bool>(() => _http.PutAsJsonAsync("api/v1/Categories", request));
            return response ?? false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await SendAsync<bool>(() => _http.DeleteAsync($"api/v1/Categories/{id}"));
            return response ?? true;
        }

        public async Task<string> GetReportAsync()
        {
            var response = await GetAsync<string>("api/v1/Categories/report");
            return response ?? string.Empty;
        }

        private async Task<T?> GetAsync<T>(string url)
        {
            AddBearerToken();
            var httpResponse = await _http.GetAsync(url);

            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            httpResponse.EnsureSuccessStatusCode();

            var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<T>>();
            return apiResponse?.Data;
        }

        private async Task<T?> SendAsync<T>(Func<Task<HttpResponseMessage>> action)
        {
            AddBearerToken();
            var httpResponse = await action();

            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            httpResponse.EnsureSuccessStatusCode();

            var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<T>>();
            return apiResponse?.Data;
        }
    }
}
