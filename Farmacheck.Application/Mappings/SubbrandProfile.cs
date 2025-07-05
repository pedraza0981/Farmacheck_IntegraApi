using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.SubBrands;

namespace Farmacheck.Application.Mappings
{
    public class SubbrandProfile : Profile
    {
        public SubbrandProfile()
        {
            CreateMap<SubbrandResponse, SubmarcaDto>()
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.Marca));

            CreateMap<SubmarcaDto, SubbrandRequest>()
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId));

            CreateMap<SubmarcaDto, UpdateSubbrandRequest>()
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId));
        }
    }
}
