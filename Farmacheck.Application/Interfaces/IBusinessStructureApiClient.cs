using Farmacheck.Application.Models.BusinessStructures;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IBusinessStructureApiClient
    {
        Task<List<BusinessStructureResponse>> GetBusinessStructuresAsync();
        Task<PaginatedResponse<BusinessStructureResponse>> GetBusinessStructuresByPageAsync(int page, int items);
        Task<BusinessStructureResponse?> GetBusinessStructureAsync(int id);
        Task<IEnumerable<BusinessStructureResponse>?> GetBusinessStructureByCustomerAsync(long customerId);
        Task<int> CreateAsync(BusinessStructureRequest request);
        Task<bool> UpdateAsync(UpdateBusinessStructureRequest request);
        Task DeleteAsync(int id);
    }
}
