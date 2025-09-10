using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class PeriodicityByQuestionnaireApiClient : IPeriodicityByQuestionnaireApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PeriodicityByQuestionnaireApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<PeriodicityByQuestionnaireResponse>>("api/v1/PeriodicityByQuestionnaire")
                   ?? new List<PeriodicityByQuestionnaireResponse>();
        }

        public async Task<PaginatedResponse<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/PeriodicityByQuestionnaire/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<PaginatedResponse<PeriodicityByQuestionnaireResponse>>(url)
                   ?? new PaginatedResponse<PeriodicityByQuestionnaireResponse>();
        }

        public async Task<PeriodicityByQuestionnaireResponse?> GetPeriodicityAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<PeriodicityByQuestionnaireResponse>($"api/v1/PeriodicityByQuestionnaire/{id}");
        }

        public async Task<bool> CreateAsync(PeriodicityByQuestionnaireRequest request)
        {
            try
            {
                AddBearerToken();
                var response = await _http.PostAsJsonAsync("api/v1/PeriodicityByQuestionnaire", request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<bool>();
            }
           
            catch (Exception ex)
            {
                // cualquier otro error (serialización, timeout, etc.)
                Console.WriteLine($"Error en CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdatePeriodicityByQuestionnaireRequest request)
        {
            try
            {
                AddBearerToken();
                var response = await _http.PutAsJsonAsync("api/v1/PeriodicityByQuestionnaire", request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<bool>();
            }
       
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateAsync: {ex.Message}");
                return false;
            }
        }


        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/PeriodicityByQuestionnaire/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
