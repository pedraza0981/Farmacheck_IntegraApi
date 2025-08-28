using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistSections;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Farmacheck.Infrastructure.Services
{
    public class ChecklistSectionApiClient : IChecklistSectionApiClient
    {
        private readonly HttpClient _http;

        public ChecklistSectionApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<ChecklistSectionResponse> GetSectionsByChecklistAsync(int? id)
        {
            return await _http.GetFromJsonAsync<ChecklistSectionResponse>($"api/v1/checklists/filters?checklistId={id}")
                   ?? new ChecklistSectionResponse();
        }

        public async Task<QuestionsBySectionResponse> GetQuestionsBySectionAsync(int cuestionarioId, int seccionId)
        {
            return await _http.GetFromJsonAsync<QuestionsBySectionResponse>($"api/v1/sections/filters?checklistId={cuestionarioId}&sectionId={seccionId}")
                   ?? new QuestionsBySectionResponse();
        }

        public async Task DeleteAsync(RemoveChecklistSectionRequest removeRequest)
        {
            string jsonContent = JsonConvert.SerializeObject(removeRequest);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/v1/sections");
            request.Content = content;
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<int> CreateAsync(ChecklistSectionRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/sections", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateChecklistSectionRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/sections", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<ChecklistSectionResponse?> GetSectionAsync(int cuestionarioId, int seccionId)
        {
            return await _http.GetFromJsonAsync<ChecklistSectionResponse>($"api/v1/checklists/filters?checklistId={cuestionarioId}&section={seccionId}");
        }
    }
}
