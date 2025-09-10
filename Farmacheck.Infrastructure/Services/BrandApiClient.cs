using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class BrandApiClient : IBrandApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IEnumerable<BrandResponse>> GetAllBrandsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<BrandResponse>>("api/v1/Brands/all")
                   ?? Enumerable.Empty<BrandResponse>();
        }

        public async Task<List<BrandResponse>> GetBrandsAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<BrandResponse>>("api/v1/Brands")
                   ?? new List<BrandResponse>();
        }

        public async Task<PaginatedResponse<BrandResponse>> GetBrandsByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/Brands/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<BrandResponse>>(url)
                      ?? new PaginatedResponse<BrandResponse>();

            return res;
        }

        public async Task<List<BrandResponse>> GetBrandsByBusinessUnitAsync(int businessUnitId)
        {
            AddBearerToken();
            var url = $"api/v1/Brands/businessunit/{businessUnitId}";
            return await _http.GetFromJsonAsync<List<BrandResponse>>(url)
                   ?? new List<BrandResponse>();
        }

        public async Task<BrandResponse?> GetBrandAsync(int? id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<BrandResponse>($"api/v1/Brands/{id}");
        }

        public async Task<int> CreateAsync(BrandRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/Brands", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateBrandRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/Brands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/Brands/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/Brands/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

