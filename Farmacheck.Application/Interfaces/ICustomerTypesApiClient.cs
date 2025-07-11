using Farmacheck.Application.Models.CustomerTypes;

namespace Farmacheck.Application.Interfaces
{
    public interface ICustomerTypesApiClient
    {
        Task<List<CustomerTypeResponse>> GetCustomerTypesAsync();
        Task<List<CustomerTypeResponse>> GetCustomerTypesByPageAsync(int page, int items);
        Task<CustomerTypeResponse?> GetCustomerTypeAsync(int id);
        Task<int> CreateAsync(CustomerTypeRequest request);
        Task<bool> UpdateAsync(UpdateCustomerTypeRequest request);
        Task DeleteAsync(int id);
    }
}
