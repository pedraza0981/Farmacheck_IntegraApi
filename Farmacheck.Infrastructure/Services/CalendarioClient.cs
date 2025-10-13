using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Calendario;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CalendarioClient : ICalendarioClient
    {
        private readonly HttpClient _http;

        public CalendarioClient(HttpClient http)
        {
            _http = http;
        }

        public Task<AddArchivoCalendarResponse> AddArchivoAsync(AddArchivoCalendarRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AddEventoCalendarResponse> AddEventoAsync(AddEventoCalendarRequest request)
        {
            using var resp = await _http.PostAsJsonAsync("api/v1/Calendar/eventos", request);
            var dto = await resp.Content.ReadFromJsonAsync<AddEventoCalendarResponse>()
                      ?? new AddEventoCalendarResponse();

            // asegura flags en base al HTTP real
            dto.Success = dto.Success || resp.IsSuccessStatusCode;
            dto.ErrorCode = dto.Success ? null : (int?)resp.StatusCode;
            dto.ErrorMessage ??= dto.Success ? null : resp.ReasonPhrase;

            return dto;
        }

        public async Task<ConfiguracionCalendarioResponse> GetConfig(int usuarioId, int calendarioId)
        {
            return await _http.GetFromJsonAsync<ConfiguracionCalendarioResponse>($"api/v1/Calendar/config?usuarioId={usuarioId}&calendarioId={calendarioId}")
                   ?? new ConfiguracionCalendarioResponse();
        }

        public async Task<IEnumerable<FullCalendarEventResponse>> GetNonRecurringAsync(int usuarioId, int anio)
        {
            return await _http.GetFromJsonAsync<IEnumerable<FullCalendarEventResponse>>($"api/v1/Calendar/anio/{anio}?usuarioId={usuarioId}")
                   ?? Enumerable.Empty<FullCalendarEventResponse>();
        }

        public async Task<IEnumerable<FullCalendarEventResponse>> GetNonRecurringAsync(int usuarioId, int anio, int mes)
        {
            return await _http.GetFromJsonAsync<IEnumerable<FullCalendarEventResponse>>($"api/v1/Calendar/anio/{anio}/mes/{mes}?usuarioId={usuarioId}")
                   ?? Enumerable.Empty<FullCalendarEventResponse>();
        }

        public async Task<bool> PutConfig(ConfiguracionCalendarioResponse request)
        {
            using var resp = await _http.PutAsJsonAsync("api/v1/Calendar/config", request);
            return resp.IsSuccessStatusCode;
        }

    }
}
