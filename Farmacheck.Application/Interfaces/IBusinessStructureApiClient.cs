using Farmacheck.Application.Models.BusinessStructures;

namespace Farmacheck.Application.Interfaces
{
    public interface IBusinessStructureApiClient
    {
        Task<List<BusinessStructureResponse>> GetBusinessStructuresAsync();
        Task<List<BusinessStructureResponse>> GetBusinessStructuresByPageAsync(int page, int items);
        Task<BusinessStructureResponse?> GetBusinessStructureAsync(int id);
        Task<IEnumerable<BusinessStructureResponse>?> GetBusinessStructureByCustomerAsync(int customerId);
        Task<int> CreateAsync(BusinessStructureRequest request);
        Task<bool> UpdateAsync(UpdateBusinessStructureRequest request);
        Task DeleteAsync(int id);
    }
}
