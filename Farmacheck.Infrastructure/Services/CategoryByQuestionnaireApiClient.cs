using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CategoryByQuestionnaireApiClient : ICategoryByQuestionnaireApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryByQuestionnaireApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<CategoryByQuestionnaireResponse>> GetAllCategoriesAsync()
        {
            AddBearerToken();
            var categories = await _http.GetFromJsonAsync<IEnumerable<CategoryByQuestionnaireResponse>>("api/v1/CategoriesByChecklists")
                   ?? Enumerable.Empty<CategoryByQuestionnaireResponse>();

            return categories.OrderBy(c => c.Nombre);
        }

        public async Task<List<CategoryByQuestionnaireResponse>> GetCategoriesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<CategoryByQuestionnaireResponse>>("api/v1/CategoriesByChecklists")
                   ?? new List<CategoryByQuestionnaireResponse>();
        }

        public async Task<PaginatedResponse<CategoryByQuestionnaireResponse>> GetCategoriesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/CategoriesByChecklists/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<CategoryByQuestionnaireResponse>>(url)
                      ?? new PaginatedResponse<CategoryByQuestionnaireResponse>();

            return res;
        }

        public async Task<CategoryByQuestionnaireResponse?> GetCategoryAsync(byte id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<CategoryByQuestionnaireResponse>($"api/v1/CategoriesByChecklists/{id}");
        }

        public async Task<CategoryByQuestionnaireResponse?> GetByNameAsync(string name)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<CategoryByQuestionnaireResponse>($"api/v1/CategoriesByChecklists/name/{name}");
        }

        public async Task<byte> CreateAsync(CategoryByQuestionnaireRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/CategoriesByChecklists", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<byte>();
        }

        public async Task<bool> UpdateAsync(UpdateCategoryByQuestionnaireRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/CategoriesByChecklists", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(byte id)
        {
            try
            {
                AddBearerToken();
                var response = await _http.DeleteAsync($"api/v1/CategoriesByChecklists/{id}");
                response.EnsureSuccessStatusCode();
            }
            
            catch (Exception ex)
            {
                // Manejo general de errores
                Console.WriteLine($"Ocurrió un error inesperado al eliminar el recurso con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
