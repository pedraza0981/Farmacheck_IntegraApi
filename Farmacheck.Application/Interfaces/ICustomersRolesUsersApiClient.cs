using System.Collections.Generic;
using Farmacheck.Application.Models.CustomersRolesUsers;

namespace Farmacheck.Application.Interfaces
{
    public interface ICustomersRolesUsersApiClient
    {
        Task<List<CustomerRolUserResponse>> GetByCustomerAsync(long client);
        Task<List<CustomerRolUserResponse>> GetAsync();
        Task<List<CustomerRolUserResponse>> GetPagesAsync(int page, int items);
        Task<List<CustomerRolUserResponse>> GetPagesByCustomerAsync(int page, int items, long customer);
        Task<CustomerRolUserResponse?> GetByIdAsync(int id);
        Task<string> CreateAsync(CustomerRolUserRequest request);
        Task<bool> UpdateAsync(UpdateCustomerRolUserRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> RemoveByCustomerAsync(List<int> ids, int customer);
    }
}
