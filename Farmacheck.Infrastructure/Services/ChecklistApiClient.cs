using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Checklists;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class ChecklistApiClient : IChecklistApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChecklistApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<ChecklistResponse>> GetAllChecklistsAsync()
        {
            AddBearerToken();
            var checklists = await _http.GetFromJsonAsync<IEnumerable<ChecklistResponse>>("api/v1/checklists")
                   ?? Enumerable.Empty<ChecklistResponse>();

            return checklists.OrderBy(c => c.Nombre);
        }

        public async Task<ChecklistSummary?> GetChecklistSummaryAsync(int? id)
        {
            return await _http.GetFromJsonAsync<ChecklistSummary>($"api/v1/checklists/summary?checklistId={id}");
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/checklists/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> CreateAsync(ChecklistRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/checklists", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateChecklistRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/checklists", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<ChecklistResponse?> GetChecklistAsync(int? id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<ChecklistResponse>($"api/v1/checklists/{id}");
        }

        public async Task<string> GetReport(int checklistId)
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/checklists/report?checklistId="+checklistId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
