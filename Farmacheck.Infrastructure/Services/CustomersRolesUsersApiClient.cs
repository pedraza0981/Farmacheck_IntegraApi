using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CustomersRolesUsers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Linq;

namespace Farmacheck.Infrastructure.Services
{
    public class CustomersRolesUsersApiClient : ICustomersRolesUsersApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomersRolesUsersApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<CustomerRolUserResponse>> GetByCustomerAsync(long client)
        {
            AddBearerToken();
            var url = $"api/v1/Customers_RolesUsers/customer?client={client}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetsByUserRolAsync(int userRolId)
        {
            AddBearerToken();
            var url = $"api/v1/Customers_RolesUsers/roluser?userRol={userRolId}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>("api/v1/Customers_RolesUsers")
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetPagesAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Customers_RolesUsers/pages?page={page}&items={items}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<List<CustomerRolUserResponse>> GetPagesByCustomerAsync(int page, int items, long customer)
        {
            AddBearerToken();
            var url = $"api/v1/Customers_RolesUsers/customer/pages?page={page}&items={items}&customer={customer}";
            return await _http.GetFromJsonAsync<List<CustomerRolUserResponse>>(url)
                   ?? new List<CustomerRolUserResponse>();
        }

        public async Task<CustomerRolUserResponse?> GetByIdAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<CustomerRolUserResponse>($"api/v1/Customers_RolesUsers/{id}");
        }

        public async Task<string> CreateAsync(CustomerRolUserRequest request)
        {
            try
            {
                AddBearerToken();
                var response = await _http.PostAsJsonAsync("api/v1/Customers_RolesUsers", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(UpdateCustomerRolUserRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Customers_RolesUsers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Customers_RolesUsers/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> RemoveByCustomerAsync(List<int> ids, long customer)
        {
            try
            {
                AddBearerToken();
                var idsParam = string.Join(",", ids);
                var url = $"api/v1/Customers_RolesUsers/customer?ids={idsParam}&customer={customer}";

                var response = await _http.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar: {response.StatusCode} - {errorMessage}");
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<bool>();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }
    }
}
