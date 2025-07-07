using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.BusinessStructures;

namespace Farmacheck.Application.Mappings
{
    public class BusinessStructureProfile : Profile
    {
        public BusinessStructureProfile()
        {
            CreateMap<BusinessStructureResponse, BusinessStructureDto>();
        }
    }
}
