using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class UsersApiClient : IUserApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<UserResponse>>("api/v1/Users/all")
                   ?? Enumerable.Empty<UserResponse>();
        }

        public async Task<List<UserResponse>> GetUsersAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<UserResponse>>("api/v1/Users")
                   ?? new List<UserResponse>();
        }

        public async Task<PaginatedResponse<UserResponse>> GetUsersByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Users/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<UserResponse>>(url)
                      ?? new PaginatedResponse<UserResponse>();

            return res;
        }

        public async Task<UserResponse?> GetUserAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<UserResponse>($"api/v1/Users/{id}");
        }

        public async Task<UserResponse?> GetUserByEmailAsync(string email)
        {
            AddBearerToken();
            var url = QueryHelpers.AddQueryString("api/v1/Users/by-email", new Dictionary<string, string?>
            {
                ["email"] = email,
            });

            return await _http.GetFromJsonAsync<UserResponse>(url);
        }

        public async Task<int> CreateAsync(UserRequest request)
        {
            try
            {

                AddBearerToken();
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
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Users", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> UpdatePasswordAsync(UpdateUserPasswordRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Users/password", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Users/{id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Users/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
