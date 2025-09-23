using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Categories;
using Microsoft.AspNetCore.Http;
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

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            AddBearerToken();
            var categories = await _http.GetFromJsonAsync<IEnumerable<CategoryResponse>>("api/v1/categories")
                   ?? Enumerable.Empty<CategoryResponse>();

            return categories.OrderBy(c => c.Nombre);
        }
    }
}
