using Farmacheck.Infrastructure.Models.BusinessStructures;

namespace Farmacheck.Infrastructure.Interfaces
{
    public interface IBusinessStructureApiClient
    {
        Task<List<BusinessStructureResponse>> GetBusinessStructuresAsync();
        Task<List<BusinessStructureResponse>> GetBusinessStructuresByPageAsync(int page, int items);
        Task<BusinessStructureResponse?> GetBusinessStructureAsync(int id);
        Task<BusinessStructureResponse?> GetBusinessStructureByCustomerAsync(int customerId);
        Task<int> CreateAsync(BusinessStructureRequest request);
        Task<bool> UpdateAsync(UpdateBusinessStructureRequest request);
        Task DeleteAsync(int id);
    }
}
