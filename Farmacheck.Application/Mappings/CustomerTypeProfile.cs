using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.CustomerTypes;

namespace Farmacheck.Application.Mappings
{
    public class CustomerTypeProfile : Profile
    {
        public CustomerTypeProfile()
        {
            CreateMap<CustomerTypeResponse, CustomerTypeDto>();
        }
    }
}
