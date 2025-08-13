using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.Common;
using System;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class BrandApiClient : IBrandApiClient
    {
        private readonly HttpClient _http;

        public BrandApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<BrandResponse>> GetBrandsAsync()
        {
            return await _http.GetFromJsonAsync<List<BrandResponse>>("api/v1/Brands")
                   ?? new List<BrandResponse>();
        }

        public async Task<PaginatedResponse<BrandResponse>> GetBrandsByPageAsync(int page, int items)
        {
            var url = $"api/v1/Brands/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<BrandResponse>>(url)
                      ?? new PaginatedResponse<BrandResponse>();

            return res;
        }

        public async Task<List<BrandResponse>> GetBrandsByBusinessUnitAsync(int businessUnitId)
        {
            var url = $"api/v1/Brands/businessunit/{businessUnitId}";
            return await _http.GetFromJsonAsync<List<BrandResponse>>(url)
                   ?? new List<BrandResponse>();
        }

        public async Task<BrandResponse?> GetBrandAsync(int? id)
        {
            return await _http.GetFromJsonAsync<BrandResponse>($"api/v1/Brands/{id}");
        }

        public async Task<int> CreateAsync(BrandRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Brands", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateBrandRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Brands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Brands/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            var response = await _http.GetAsync("api/v1/Brands/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
