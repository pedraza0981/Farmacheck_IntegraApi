using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.Checklists;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class ChecklistApiClient : IChecklistApiClient
    {
        private readonly HttpClient _http;

        public ChecklistApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ChecklistResponse>> GetAllChecklistsAsync()
        {
            var checklists = await _http.GetFromJsonAsync<IEnumerable<ChecklistResponse>>("api/v1/checklists")
                   ?? Enumerable.Empty<ChecklistResponse>();

            return checklists.OrderBy(c => c.Nombre);
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/checklists/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> CreateAsync(ChecklistRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/checklists", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateChecklistRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/checklists", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<ChecklistResponse?> GetChecklistAsync(int? id)
        {
            return await _http.GetFromJsonAsync<ChecklistResponse>($"api/v1/checklists/{id}");
        }

        public async Task<string> GetReport(int checklistId)
        {
            var response = await _http.GetAsync("api/v1/checklists/report?checklistId="+checklistId);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
