using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Brands;

namespace Farmacheck.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandRequest, MarcaRequestDto>()
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));


            CreateMap<BrandResponse, MarcaDto>()
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocio))
                .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo));

            CreateMap<MarcaDto, BrandRequest>()
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId))
                .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia))
                .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));


        }
    }
}
