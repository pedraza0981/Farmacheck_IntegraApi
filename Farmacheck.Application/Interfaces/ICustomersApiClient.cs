using Farmacheck.Application.Models.Customers;

namespace Farmacheck.Application.Interfaces
{
    public interface ICustomersApiClient
    {
        Task<List<CustomerResponse>> GetCustomersAsync();
        Task<List<CustomerResponse>> GetCustomersByPageAsync(int page, int items);
        Task<CustomerResponse?> GetCustomerAsync(int id);
        Task<int> CreateAsync(CustomerRequest request);
        Task<bool> UpdateAsync(UpdateCustomerRequest request);
        Task DeleteAsync(int id);
    }
}
