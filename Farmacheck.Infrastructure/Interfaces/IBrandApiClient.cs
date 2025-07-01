using Farmacheck.Infrastructure.Models.Brands;

namespace Farmacheck.Infrastructure.Interfaces
{
    public interface IBrandApiClient
    {
        Task<List<BrandResponse>> GetBrandsAsync(); 
        Task<BrandResponse?> GetBrandAsync(int id); 
        Task<int> CreateAsync(BrandRequest request);
        Task DeleteAsync(int id);
    }
}
