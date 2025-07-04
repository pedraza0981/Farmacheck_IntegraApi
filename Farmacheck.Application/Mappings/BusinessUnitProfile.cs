using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.BusinessUnits;

namespace Farmacheck.Application.Mappings
{
    public class BusinessUnitProfile : Profile
    {
        public BusinessUnitProfile()
        {
            CreateMap<BusinessUnitResponse, BusinessUnitDto>();
            CreateMap<BusinessUnitDto, BusinessUnitRequest>();
        }
    }
}
