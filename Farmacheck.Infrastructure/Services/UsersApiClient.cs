using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class UsersApiClient : IUserApiClient
    {
        private readonly HttpClient _http;

        public UsersApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<UserResponse>>("api/v1/Users/all")
                   ?? Enumerable.Empty<UserResponse>();
        }

        public async Task<List<UserResponse>> GetUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserResponse>>("api/v1/Users")
                   ?? new List<UserResponse>();
        }

        public async Task<PaginatedResponse<UserResponse>> GetUsersByPageAsync(int page, int items)
        {
            var url = $"api/v1/Users/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<UserResponse>>(url)
                      ?? new PaginatedResponse<UserResponse>();

            return res;
        }

        public async Task<UserResponse?> GetUserAsync(int id)
        {
            return await _http.GetFromJsonAsync<UserResponse>($"api/v1/Users/{id}");
        }

        public async Task<int> CreateAsync(UserRequest request)
        {
            try
            {

                var response = await _http.PostAsJsonAsync("api/v1/Users", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<int>();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado al crear el usuario.", ex);
            }
        }

        public async Task<bool> UpdateAsync(UpdateUserRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Users", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Users/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> GetReport()
        {
            var response = await _http.GetAsync("api/v1/Users/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
