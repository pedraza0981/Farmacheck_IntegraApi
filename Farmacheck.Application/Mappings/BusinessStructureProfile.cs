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

            CreateMap<BusinessStructureDto, BusinessStructureRequest>()
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId))
                .ForMember(dest => dest.SubmarcaId, opt => opt.MapFrom(src => src.SubmarcaId))
                .ForMember(dest => dest.ZonaId, opt => opt.MapFrom(src => src.ZonaId))
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore());

            CreateMap<BusinessStructureDto, UpdateBusinessStructureRequest>()
                .IncludeBase<BusinessStructureDto, BusinessStructureRequest>();

            CreateMap<BusinessStructureRequestDto, BusinessStructureRequest>();

            CreateMap<BusinessStructureRequestDto, UpdateBusinessStructureRequest>()
                .IncludeBase<BusinessStructureRequestDto, BusinessStructureRequest>();
        }
    }
}
