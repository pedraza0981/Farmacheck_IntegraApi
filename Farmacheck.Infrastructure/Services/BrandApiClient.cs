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
            var serverPage = page > 0 ? page - 1 : 0;
            var url = $"api/v1/Brands/pages?page={serverPage}&items={items}";
            var result = await _http.GetFromJsonAsync<PaginatedResponse<BrandResponse>>(url);

            if (result is null)
            {
                return new PaginatedResponse<BrandResponse>(Array.Empty<BrandResponse>(), 0, page, items);
            }

            return new PaginatedResponse<BrandResponse>(result.Items, result.TotalCount, result.CurrentPage + 1, result.PageSize);
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
    }
}
