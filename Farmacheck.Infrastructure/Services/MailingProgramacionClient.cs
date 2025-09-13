
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.MailingProgramacion;
using Farmacheck.Application.Models.ZonaHorario;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class MailingProgramacionClient : IMailingProgramacionClient
    {
        private readonly HttpClient _http;

        public MailingProgramacionClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<int> CreateAsync(MailingProgramacionRequest request)
        {
            using var response = await _http.PostAsJsonAsync(
                "api/v1/MailingProgramacion/RegisterView", request);

            if (!response.IsSuccessStatusCode)
                return 0;

            // Si el body es solo el ID: 123
            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/MailingProgramacion/{id}");

            if (!response.IsSuccessStatusCode)
                return false;

            return await response.Content.ReadFromJsonAsync<bool>() ? true : false;
        }

        public async Task<IEnumerable<vMailingProgramacionWebResponse>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<vMailingProgramacionWebResponse>>("api/v1/MailingProgramacion/View/Programacion/All")
                   ?? Enumerable.Empty<vMailingProgramacionWebResponse>();
        }

        public async Task<IEnumerable<PeriodicidadResponse>> GetPeriodicidadAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<PeriodicidadResponse>>("api/v1/Periodicidad")
                   ?? Enumerable.Empty<PeriodicidadResponse>();
        }

        public async Task<IEnumerable<ZonaHorarioResponse>> GetZonaHorarioAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ZonaHorarioResponse>>("api/v1/ZonaHoraria")
                   ?? Enumerable.Empty<ZonaHorarioResponse>();
        }
        public async Task<IEnumerable<TipoReporteResponse>> GetTipoReporteAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<TipoReporteResponse>>("api/v1/TipoReporte")
                   ?? Enumerable.Empty<TipoReporteResponse>();
        }

        public Task<vMailingProgramacionWebResponse?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResponse<vMailingProgramacionWebResponse>> GetByPageAsync(int page, int items)
        {
            throw new NotImplementedException();
        }
    }
}
