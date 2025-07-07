using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.Customers;

namespace Farmacheck.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerResponse, CustomerDto>();
            CreateMap<BusinessStructureResponse, BusinessStructureDto>();

            CreateMap<CustomerDto, CustomerRequest>();
            CreateMap<CustomerDto, UpdateCustomerRequest>();
        }
    }
}
