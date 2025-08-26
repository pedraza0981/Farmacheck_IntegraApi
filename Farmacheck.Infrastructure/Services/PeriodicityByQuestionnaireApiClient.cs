using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PeriodicityByQuestionnaireApiClient : IPeriodicityByQuestionnaireApiClient
    {
        private readonly HttpClient _http;

        public PeriodicityByQuestionnaireApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesAsync()
        {
            return await _http.GetFromJsonAsync<List<PeriodicityByQuestionnaireResponse>>("api/v1/PeriodicityByQuestionnaire")
                   ?? new List<PeriodicityByQuestionnaireResponse>();
        }

        public async Task<PaginatedResponse<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesByPageAsync(int page, int items)
        {
            var url = $"api/v1/PeriodicityByQuestionnaire/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<PeriodicityByQuestionnaireResponse>>(url)
                   ?? new PaginatedResponse<PeriodicityByQuestionnaireResponse>();
        }

        public async Task<PeriodicityByQuestionnaireResponse?> GetPeriodicityAsync(int id)
        {
            return await _http.GetFromJsonAsync<PeriodicityByQuestionnaireResponse>($"api/v1/PeriodicityByQuestionnaire/{id}");
        }

        public async Task<int> CreateAsync(PeriodicityByQuestionnaireRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/PeriodicityByQuestionnaire", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdatePeriodicityByQuestionnaireRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/PeriodicityByQuestionnaire", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/PeriodicityByQuestionnaire/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
