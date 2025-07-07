using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.SubBrands;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class SubbrandApiClient : ISubbrandApiClient
    {
        private readonly HttpClient _http;

        public SubbrandApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<SubbrandResponse>> GetSubbrandsAsync()
        {
            return await _http.GetFromJsonAsync<List<SubbrandResponse>>("api/v1/Subbrands")
                   ?? new List<SubbrandResponse>();
        }

        public async Task<List<SubbrandResponse>> GetSubbrandsByPageAsync(int page, int items)
        {
            return await _http.GetFromJsonAsync<List<SubbrandResponse>>($"api/v1/Subbrands/pages?page={page}&items={items}")
                   ?? new List<SubbrandResponse>();
        }
        public async Task<List<SubbrandResponse>> GetSubbrandsByBrandAsync(int brandId)
        {
            var url = $"api/v1/Subbrands/brand/{brandId}";
            return await _http.GetFromJsonAsync<List<SubbrandResponse>>(url)
                   ?? new List<SubbrandResponse>();
        }
        public async Task<SubbrandResponse?> GetSubbrandAsync(int id)
        {
            return await _http.GetFromJsonAsync<SubbrandResponse>($"api/v1/Subbrands/{id}");
        }

        public async Task<int> CreateAsync(SubbrandRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/Subbrands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateSubbrandRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/Subbrands", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/Subbrands/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
