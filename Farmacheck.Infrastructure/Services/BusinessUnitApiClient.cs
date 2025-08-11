using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.BusinessUnits;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class BusinessUnitApiClient : IBusinessUnitApiClient
    {
        private readonly HttpClient _http;

        public BusinessUnitApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<BusinessUnitResponse>> GetBusinessUnitsAsync()
        {
            return await _http.GetFromJsonAsync<List<BusinessUnitResponse>>("api/v1/BusinessUnits")
                   ?? new List<BusinessUnitResponse>();
        }

        public async Task<PaginatedResponse<BusinessUnitResponse>> GetBusinessUnitsByPageAsync(int page, int items)
        {
            var url = $"api/v1/BusinessUnits/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<BusinessUnitResponse>>(url)
                      ?? new PaginatedResponse<BusinessUnitResponse>();

            return res;
        }

        public async Task<BusinessUnitResponse?> GetBusinessUnitAsync(int id)
        {
            return await _http.GetFromJsonAsync<BusinessUnitResponse>($"api/v1/BusinessUnits/{id}");
        }

        public async Task<int> CreateAsync(BusinessUnitRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/BusinessUnits", request);
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(BusinessUnitRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/BusinessUnits", request);
            response.EnsureSuccessStatusCode();
            var updated = await response.Content.ReadFromJsonAsync<bool>();
            return updated;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/BusinessUnits/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
