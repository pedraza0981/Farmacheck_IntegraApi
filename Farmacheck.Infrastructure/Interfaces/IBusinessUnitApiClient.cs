using Farmacheck.Infrastructure.Models.BusinessUnits;

namespace Farmacheck.Infrastructure.Interfaces
{
    public interface IBusinessUnitApiClient
    {
        Task<List<BusinessUnitResponse>> GetBusinessUnitsAsync();
        Task<List<BusinessUnitResponse>> GetBusinessUnitsByPageAsync(int page, int items);
        Task<BusinessUnitResponse?> GetBusinessUnitAsync(int id);
        Task<int> CreateAsync(BusinessUnitRequest request);
        Task<bool> UpdateAsync(BusinessUnitRequest request);
        Task DeleteAsync(int id);
    }
}
