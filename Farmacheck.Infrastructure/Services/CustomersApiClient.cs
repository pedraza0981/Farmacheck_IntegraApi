using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.Customers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class CustomersApiClient : ICustomersApiClient
    {
        private readonly HttpClient _http;

        public CustomersApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CustomerResponse>> GetCustomersAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomerResponse>>("api/v1/Customers")
                   ?? new List<CustomerResponse>();
        }

        public async Task<List<CustomerResponse>> GetCustomersByPageAsync(int page, int items)
        {
            var url = $"api/v1/Customers/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<CustomerResponse>>(url)
                   ?? new List<CustomerResponse>();
        }

        public async Task<CustomerResponse?> GetCustomerAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomerResponse>($"api/v1/Customers/{id}");
        }

        public async Task<int> CreateAsync(CustomerRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Customers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateCustomerRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Customers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Customers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
