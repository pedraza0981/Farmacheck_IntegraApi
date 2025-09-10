using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.SubBrands;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class SubbrandApiClient : ISubbrandApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubbrandApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<SubbrandResponse>> GetAllSubbrandsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<SubbrandResponse>>("api/v1/Subbrands/all")
                   ?? Enumerable.Empty<SubbrandResponse>();
        }

        public async Task<List<SubbrandResponse>> GetSubbrandsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<SubbrandResponse>>("api/v1/Subbrands")
                   ?? new List<SubbrandResponse>();
        }

        public async Task<PaginatedResponse<SubbrandResponse>> GetSubbrandsByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Subbrands/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<SubbrandResponse>>(url)
                      ?? new PaginatedResponse<SubbrandResponse>();

            return res;
        }
        public async Task<List<SubbrandResponse>> GetSubbrandsByBrandsAsync(List<int> brands)
        {
            AddBearerToken();
            var query = string.Join("&", brands.Select(b => $"brands={b}"));
            var url = $"api/v1/Subbrands/brand?{query}";
            return await _http.GetFromJsonAsync<List<SubbrandResponse>>(url)
                   ?? new List<SubbrandResponse>();
        }
        public async Task<SubbrandResponse?> GetSubbrandAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<SubbrandResponse>($"api/v1/Subbrands/{id}");
        }

        public async Task<int> CreateAsync(SubbrandRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Subbrands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateSubbrandRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Subbrands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Subbrands/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Subbrands/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
