
using Farmacheck.Infrastructure.Models.SubBrands;

namespace Farmacheck.Infrastructure.Interfaces
{
    public interface ISubbrandApiClient
    {
        Task<List<SubbrandResponse>> GetSubbrandsAsync();
        Task<List<SubbrandResponse>> GetSubbrandsByPageAsync(int page, int items);
        Task<List<SubbrandResponse>> GetSubbrandsByBrandAsync(int brandId);
        Task<SubbrandResponse?> GetSubbrandAsync(int id);
        Task<int> CreateAsync(SubbrandRequest request);
        Task<bool> UpdateAsync(UpdateSubbrandRequest request);
        Task DeleteAsync(int id);
    }
}
