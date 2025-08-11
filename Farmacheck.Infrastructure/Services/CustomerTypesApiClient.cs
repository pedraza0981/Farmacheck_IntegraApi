using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CustomerTypes;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CustomerTypesApiClient : ICustomerTypesApiClient
    {
        private readonly HttpClient _http;

        public CustomerTypesApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CustomerTypeResponse>> GetCustomerTypesAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomerTypeResponse>>("api/v1/CustomerTypes")
                   ?? new List<CustomerTypeResponse>();
        }

        public async Task<PaginatedResponse<CustomerTypeResponse>> GetCustomerTypesByPageAsync(int page, int items)
        {
            var url = $"api/v1/CustomerTypes/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<CustomerTypeResponse>>(url)
                      ?? new PaginatedResponse<CustomerTypeResponse>();

            return res;
        }

        public async Task<CustomerTypeResponse?> GetCustomerTypeAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomerTypeResponse>($"api/v1/CustomerTypes/{id}");
        }

        public async Task<int> CreateAsync(CustomerTypeRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/CustomerTypes", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateCustomerTypeRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/CustomerTypes", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/CustomerTypes/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
