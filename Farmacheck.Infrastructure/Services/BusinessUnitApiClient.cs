using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.BusinessUnits;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class BusinessUnitApiClient : IBusinessUnitApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BusinessUnitApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<BusinessUnitResponse>> GetAllBusinessUnitsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<BusinessUnitResponse>>("api/v1/BusinessUnits/all")
                   ?? Enumerable.Empty<BusinessUnitResponse>();
        }

        public async Task<List<BusinessUnitResponse>> GetBusinessUnitsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<BusinessUnitResponse>>("api/v1/BusinessUnits")
                   ?? new List<BusinessUnitResponse>();
        }

        public async Task<PaginatedResponse<BusinessUnitResponse>> GetBusinessUnitsByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/BusinessUnits/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<BusinessUnitResponse>>(url)
                      ?? new PaginatedResponse<BusinessUnitResponse>();

            return res;
        }

        public async Task<BusinessUnitResponse?> GetBusinessUnitAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<BusinessUnitResponse>($"api/v1/BusinessUnits/{id}");
        }

        public async Task<int> CreateAsync(BusinessUnitRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/BusinessUnits", request);
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(BusinessUnitRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/BusinessUnits", request);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<bool>();
            return updated;
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/BusinessUnits/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/BusinessUnits/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
