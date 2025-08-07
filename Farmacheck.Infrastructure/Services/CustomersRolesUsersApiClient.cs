using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CustomersRolesUsers;
using System.Net.Http.Json;
using System.Linq;

namespace Farmacheck.Infrastructure.Services
{
    public class CustomersRolesUsersApiClient : ICustomersRolesUsersApiClient
    {
        private readonly HttpClient _http;

        public CustomersRolesUsersApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CustomerRolUserResponse>> GetByCustomerAsync(long client)
        {
            var url = $"api/v1/Customers_RolesUsers/customer?client={client}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetsByUserRolAsync(int rolPorUsuarioId)
        {
            var url = $"api/v1/Customers_RolesUsers/rolPorUsuario?rolPorUsuario={rolPorUsuarioId}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetAsync()
        {
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>("api/v1/Customers_RolesUsers")
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetPagesAsync(int page, int items)
        {
            var url = $"api/v1/Customers_RolesUsers/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetPagesByCustomerAsync(int page, int items, long customer)
        {
            var url = $"api/v1/Customers_RolesUsers/customer/pages?page={page}&items={items}&customer={customer}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<CustomerRolUserResponse?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CustomerRolUserResponse>($"api/v1/Customers_RolesUsers/{id}");
        }

        public async Task<string> CreateAsync(CustomerRolUserRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Customers_RolesUsers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> UpdateAsync(UpdateCustomerRolUserRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Customers_RolesUsers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Customers_RolesUsers/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> RemoveByCustomerAsync(List<int> ids, int customer)
        {
            var query = string.Join("&", ids.Select(id => $"ids={id}"));
            var url = $"api/v1/Customers_RolesUsers/customer?{query}&customer={customer}";
            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
