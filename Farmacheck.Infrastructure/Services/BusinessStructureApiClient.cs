using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.BusinessStructures;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class BusinessStructureApiClient : IBusinessStructureApiClient
    {
        private readonly HttpClient _http;

        public BusinessStructureApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<BusinessStructureResponse>> GetBusinessStructuresAsync()
        {
            return await _http.GetFromJsonAsync<List<BusinessStructureResponse>>("api/v1/BusinessStructure")
                   ?? new List<BusinessStructureResponse>();
        }

        public async Task<List<BusinessStructureResponse>> GetBusinessStructuresByPageAsync(int page, int items)
        {
            var url = $"api/v1/BusinessStructure/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<BusinessStructureResponse>>(url)
                   ?? new List<BusinessStructureResponse>();
        }

        public async Task<BusinessStructureResponse?> GetBusinessStructureAsync(int id)
        {
            return await _http.GetFromJsonAsync<BusinessStructureResponse>($"api/v1/BusinessStructure/{id}");
        }

        public async Task<IEnumerable<BusinessStructureResponse>?> GetBusinessStructureByCustomerAsync(int customerId)
        {
            try
            {
                var url = $"api/v1/BusinessStructure/customer/{customerId}";
                return await _http.GetFromJsonAsync<IEnumerable<BusinessStructureResponse>>(url);
            }
            catch (Exception ex)
            {
                // Puedes loguear ex.Message si necesitas rastrear el error
                return null;
            }
        }


        public async Task<int> CreateAsync(BusinessStructureRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/BusinessStructure", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateBusinessStructureRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/BusinessStructure", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/BusinessStructure/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
