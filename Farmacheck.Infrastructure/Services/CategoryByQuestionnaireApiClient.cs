using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CategoryByQuestionnaireApiClient : ICategoryByQuestionnaireApiClient
    {
        private readonly HttpClient _http;

        public CategoryByQuestionnaireApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryByQuestionnaireResponse>> GetCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoryByQuestionnaireResponse>>("api/v1/CategoriesByChecklists")
                   ?? new List<CategoryByQuestionnaireResponse>();
        }

        public async Task<PaginatedResponse<CategoryByQuestionnaireResponse>> GetCategoriesByPageAsync(int page, int items)
        {
            var url = $"api/v1/CategoriesByChecklists/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<CategoryByQuestionnaireResponse>>(url)
                      ?? new PaginatedResponse<CategoryByQuestionnaireResponse>();

            return res;
        }

        public async Task<CategoryByQuestionnaireResponse?> GetCategoryAsync(byte id)
        {
            return await _http.GetFromJsonAsync<CategoryByQuestionnaireResponse>($"api/v1/CategoriesByChecklists/{id}");
        }

        public async Task<byte> CreateAsync(CategoryByQuestionnaireRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/CategoriesByChecklists", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<byte>();
        }

        public async Task<bool> UpdateAsync(UpdateCategoryByQuestionnaireRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/CategoriesByChecklists", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(byte id)
        {
            try
            {
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
