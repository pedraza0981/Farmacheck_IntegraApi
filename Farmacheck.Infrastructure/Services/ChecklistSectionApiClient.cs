using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistSections;
using Farmacheck.Application.Models.GenericResponse;
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

        public async Task<QuestionsBySectionResponse> GetSectionByNameAsync(int cuestionarioId, string nombre)
        {
            var response = await _http.GetFromJsonAsync<QuestionsBySectionResponse>($"api/v1/sections/filters?checklistId={cuestionarioId}&sectionName={nombre}")
                   ?? new QuestionsBySectionResponse();

            return response;
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

        public async Task<RegisterResponse> CreateAsync(ChecklistSectionRequest request)
        {
            var registerResponse = new RegisterResponse();
            var response = await _http.PostAsJsonAsync("api/v1/sections", request);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                registerResponse.Message = "Ya existe un registro con el mismo nombre";
                return registerResponse;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                registerResponse.Id = await response.Content.ReadFromJsonAsync<int>();
                return registerResponse;
            }

            registerResponse.Message = "Hubo un error al guardar";
            return registerResponse;
        }

        public async Task<UpdateResponse> UpdateAsync(UpdateChecklistSectionRequest request)
        {
            var updateResponse = new UpdateResponse();
            var response = await _http.PutAsJsonAsync("api/v1/sections", request);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                updateResponse.Message = "Ya existe un registro con el mismo nombre";
                return updateResponse;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                updateResponse.Updated = await response.Content.ReadFromJsonAsync<bool>();
                return updateResponse;
            }

            updateResponse.Message = "Hubo un error al guardar";
            return updateResponse;
        }

        public async Task<ChecklistSectionResponse?> GetSectionAsync(int cuestionarioId, int seccionId)
        {
            return await _http.GetFromJsonAsync<ChecklistSectionResponse>($"api/v1/checklists/filters?checklistId={cuestionarioId}&section={seccionId}");
        }
    }
}
