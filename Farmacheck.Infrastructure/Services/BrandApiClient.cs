using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<BrandResponse?> GetBrandAsync(int id)
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

        public async Task DeleteAsync(int id)
        {            
            var response = await _http.DeleteAsync($"api/v1/Brands/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
