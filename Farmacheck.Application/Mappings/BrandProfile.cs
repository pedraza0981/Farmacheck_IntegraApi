using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Brands;

namespace Farmacheck.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandRequest, MarcaRequestDto>();


            CreateMap<BrandResponse, MarcaDto>()           
           
           .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocio));

            CreateMap<MarcaDto, BrandRequest>()
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId));


        }
    }
}
