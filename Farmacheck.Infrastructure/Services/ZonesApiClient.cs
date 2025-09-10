using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Zones;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class ZonesApiClient : IZoneApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ZonesApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<ZoneResponse>> GetAllZonesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<ZoneResponse>>("api/v1/Zones/all")
                   ?? Enumerable.Empty<ZoneResponse>();
        }

        public async Task<List<ZoneResponse>> GetZonesAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<ZoneResponse>>("api/v1/Zones")
                   ?? new List<ZoneResponse>();
        }

        public async Task<PaginatedResponse<ZoneResponse>> GetZonesByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Zones/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<ZoneResponse>>(url)
                      ?? new PaginatedResponse<ZoneResponse>();

            return res;
        }

        public async Task<ZoneResponse?> GetZoneAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<ZoneResponse>($"api/v1/Zones/{id}");
        }

        public async Task<int> CreateAsync(ZoneRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Zones", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateZoneRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Zones", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Zones/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Zones/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


    }
}
