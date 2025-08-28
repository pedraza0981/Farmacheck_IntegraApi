using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Questions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Farmacheck.Infrastructure.Services
{
    public class QuestionApiClient : IQuestionApiClient
    {
        private readonly HttpClient _http;

        public QuestionApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<int> CreateAsync(QuestionRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/questions", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task DeleteAsync(RemoveQuestionRequest removeRequest)
        {
            string jsonContent = JsonConvert.SerializeObject(removeRequest);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/v1/questions");
            request.Content = content;
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<QuestionResponse?> GetQuestionAsync(int cuestionarioId, int seccionId, int questionId)
        {
            return await _http.GetFromJsonAsync<QuestionResponse>($"api/v1/questions/filters?checklistId={cuestionarioId}&sectionId={seccionId}&questionId={questionId}");
        }

        public async Task<bool> UpdateAsync(UpdateQuestionRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/questions", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
