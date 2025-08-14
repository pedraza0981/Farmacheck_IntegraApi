using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Zones;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class ZonesApiClient : IZoneApiClient
    {
        private readonly HttpClient _http;

        public ZonesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ZoneResponse>> GetAllZonesAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ZoneResponse>>("api/v1/Zones/all")
                   ?? Enumerable.Empty<ZoneResponse>();
        }

        public async Task<List<ZoneResponse>> GetZonesAsync()
        {
            return await _http.GetFromJsonAsync<List<ZoneResponse>>("api/v1/Zones")
                   ?? new List<ZoneResponse>();
        }

        public async Task<PaginatedResponse<ZoneResponse>> GetZonesByPageAsync(int page, int items)
        {
            var url = $"api/v1/Zones/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<ZoneResponse>>(url)
                      ?? new PaginatedResponse<ZoneResponse>();

            return res;
        }

        public async Task<ZoneResponse?> GetZoneAsync(int id)
        {
            return await _http.GetFromJsonAsync<ZoneResponse>($"api/v1/Zones/{id}");
        }

        public async Task<int> CreateAsync(ZoneRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Zones", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateZoneRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Zones", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Zones/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
