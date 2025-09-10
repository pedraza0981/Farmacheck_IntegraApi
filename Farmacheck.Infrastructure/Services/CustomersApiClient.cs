using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Customers;
using Farmacheck.Application.Models.BusinessUnits;
using System.Net.Http.Json;
using System.Linq;
using Farmacheck.Application.Models.BusinessUnits;
using System.Text.Json;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class CustomersApiClient : ICustomersApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomersApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<CustomerResponse>>("api/v1/Customers/all")
                   ?? Enumerable.Empty<CustomerResponse>();
        }

        public async Task<List<CustomerResponse>> GetCustomersAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<CustomerResponse>>("api/v1/Customers")
                   ?? new List<CustomerResponse>();
        }

        public async Task<PaginatedResponse<CustomerResponse>> GetCustomersByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Customers/pages?page={page}&items={items}";
            var res= await _http.GetFromJsonAsync<PaginatedResponse<CustomerResponse>>(url)
                   ?? new PaginatedResponse<CustomerResponse>();

            return res;
        }

        public async Task<List<CustomerResponse>> GetCustomersByFiltersAsync( IEnumerable<int>? brand, IEnumerable<int>? subbrand, IEnumerable<int>? zone)
        {
            AddBearerToken();
            var query = new List<string>();

            if (brand != null && brand.Any())
                query.Add(string.Join("&", brand.Select(b => $"brand={b}")));

            if (subbrand != null && subbrand.Any())
                query.Add(string.Join("&", subbrand.Select(s => $"subbrand={s}")));

            if (zone != null && zone.Any())
                query.Add(string.Join("&", zone.Select(z => $"zone={z}")));

            var url = "api/v1/Customers/filters";
            if (query.Any())
                url += "?" + string.Join("&", query);

            return await _http.GetFromJsonAsync<List<CustomerResponse>>(url)
                   ?? new List<CustomerResponse>();
        }

        public async Task<List<BusinessUnitResponse>> GetBusinessUnitsByRoleAsync(int rolByUser)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<BusinessUnitResponse>>($"api/v1/Customers/rolesPorUsuario/{rolByUser}")
                   ?? new List<BusinessUnitResponse>();
        }
        public async Task<CustomerResponse?> GetCustomerAsync(int id)
        {
            try
            {
                AddBearerToken();
                return await _http.GetFromJsonAsync<CustomerResponse>($"api/v1/Customers/{id}");
            }
            
            catch (Exception ex)
            {
                // Catch-all for any other exceptions
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return null;
        }


        public async Task<int> CreateAsync(CustomerRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Customers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateCustomerRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Customers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Customers/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Customers/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
