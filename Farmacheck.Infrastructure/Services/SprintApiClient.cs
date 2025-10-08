using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Sprints;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class SprintApiClient : ISprintApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private List<SprintResponse> sprints = new List<SprintResponse>()
            {
                new SprintResponse {
                    Id = 1,
                    UnidadDeNegocioId = 1,
                    Titulo = "Titulo del Sprint con Id 1",
                    VigenciaDel = DateTime.Now,
                    VigenciaAl = DateTime.Now,
                    RequiereRevision = true,
                    PeriodoDeRevision = 3,
                    Estatus = true
                },
                new SprintResponse {
                    Id = 2,
                    UnidadDeNegocioId = 2,
                    Titulo = "Titulo del Sprint con Id 2",
                    VigenciaDel = DateTime.Now,
                    VigenciaAl = DateTime.Now,
                    RequiereRevision = true,
                    PeriodoDeRevision = 3,
                    Estatus = true
                },
                new SprintResponse {
                    Id = 3,
                    UnidadDeNegocioId = 3,
                    Titulo = "Titulo del Sprint con Id 3",
                    VigenciaDel = DateTime.Now,
                    VigenciaAl = DateTime.Now,
                    RequiereRevision = true,
                    PeriodoDeRevision = 3,
                    Estatus = true
                },
                new SprintResponse {
                    Id = 4,
                    UnidadDeNegocioId = 1,
                    Titulo = "Titulo del Sprint con Id 4",
                    VigenciaDel = DateTime.Now,
                    VigenciaAl = DateTime.Now,
                    RequiereRevision = true,
                    PeriodoDeRevision = 3,
                    Estatus = true
                }
            };

        public SprintApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<SprintResponse>> GetSprintsAsync()
        {
            AddBearerToken();
            
            return await _http.GetFromJsonAsync<List<SprintResponse>>("api/v1/sprints")
            ?? new List<SprintResponse>();
        }

        public async Task<SprintResponse?> GetSprintAsync(int? id)
        {
            AddBearerToken();

            return await _http.GetFromJsonAsync<SprintResponse>($"api/v1/sprints/{id}");
        }

        public async Task<int> CreateAsync(SprintRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/sprints", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();

            return id;
        }

        public async Task<bool> UpdateAsync(UpdateSprintRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/sprints", request);
            response.EnsureSuccessStatusCode(); 

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/sprints/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/sprints/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
