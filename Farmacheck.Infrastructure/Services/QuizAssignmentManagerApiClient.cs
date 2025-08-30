using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.QuizAssignmentManager;
using System.Net.Http.Json;
using System.Linq;

namespace Farmacheck.Infrastructure.Services
{
    public class QuizAssignmentManagerApiClient : IQuizAssignmentManagerApiClient
    {
        private readonly HttpClient _http;

        public QuizAssignmentManagerApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<QuizAssignmentManagerResponse?> GetQuizAssignmentAsync(int questionaryId)
        {
            return await _http.GetFromJsonAsync<QuizAssignmentManagerResponse>($"api/v1/QuizAssignmentManager?questionaryId={questionaryId}");
        }

        public async Task<bool> CreateAsync(QuizAssignmentManagerRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/QuizAssignmentManager", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int questionaryId, List<int> asignacionPorSupervisor, List<int> asignacionDeAuditados, List<int> asignacionPorAuditor)
        {
            var queryParts = new List<string>();

            if (asignacionPorSupervisor != null && asignacionPorSupervisor.Any())
                queryParts.Add(string.Join("&", asignacionPorSupervisor.Select(id => $"asignacionPorSupervisor={id}")));
            if (asignacionDeAuditados != null && asignacionDeAuditados.Any())
                queryParts.Add(string.Join("&", asignacionDeAuditados.Select(id => $"asignacionDeAuditados={id}")));
            if (asignacionPorAuditor != null && asignacionPorAuditor.Any())
                queryParts.Add(string.Join("&", asignacionPorAuditor.Select(id => $"asignacionPorAuditor={id}")));

            var url = $"api/v1/QuizAssignmentManager?questionaryId={questionaryId}";
            if (queryParts.Any())
                url += "&" + string.Join("&", queryParts);

            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
