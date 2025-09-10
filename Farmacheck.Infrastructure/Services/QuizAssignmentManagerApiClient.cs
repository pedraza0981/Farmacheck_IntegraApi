using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.QuizAssignmentManager;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class QuizAssignmentManagerApiClient : IQuizAssignmentManagerApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuizAssignmentManagerApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<QuizAssignmentManagerResponse?> GetQuizAssignmentAsync(int questionaryId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<QuizAssignmentManagerResponse>($"api/v1/QuizAssignmentManager?questionaryId={questionaryId}");
        }

        public async Task<bool> CreateAsync(QuizAssignmentManagerRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/QuizAssignmentManager", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int questionaryId, List<int> asignacionPorSupervisor, List<int> asignacionDeAuditados, List<int> asignacionPorAuditor)
        {
            AddBearerToken();
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
