
using Farmacheck.Application.Models.SubBrands;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface ISubbrandApiClient
    {
        Task<IEnumerable<SubbrandResponse>> GetAllSubbrandsAsync();
        Task<List<SubbrandResponse>> GetSubbrandsAsync();
        Task<PaginatedResponse<SubbrandResponse>> GetSubbrandsByPageAsync(int page, int items);
        Task<List<SubbrandResponse>> GetSubbrandsByBrandsAsync(List<int> brands);
        Task<SubbrandResponse?> GetSubbrandAsync(int id);
        Task<int> CreateAsync(SubbrandRequest request);
        Task<bool> UpdateAsync(UpdateSubbrandRequest request);
        Task DeleteAsync(int id);
    }
}
