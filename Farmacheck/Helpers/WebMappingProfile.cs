using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Infrastructure.Models.BusinessUnits;
using Farmacheck.Models;
using Farmacheck.Infrastructure.Models.Brands;
using Farmacheck.Infrastructure.Models.SubBrands;

namespace Farmacheck.Helpers
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<MarcaDto, MarcaViewModel>();

            //CreateMap<MarcaViewModel, BrandRequest>();
            //CreateMap<MarcaViewModel, UpdateBrandRequest>();

            CreateMap<BusinessUnitDto, UnidadDeNegocio>()
            .ForMember(dest => dest.LogotipoNombreArchivo, opt => opt.Ignore()) // No viene del DTO
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo ?? string.Empty))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.Rfc ?? string.Empty))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion ?? string.Empty));


            CreateMap<SubmarcaDto, SubMarca>();
            CreateMap<SubMarca, SubbrandRequest>();
            CreateMap<SubMarca, UpdateSubbrandRequest>();


            CreateMap<UnidadDeNegocio, BusinessUnitRequest>()
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo ?? string.Empty))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.Rfc ?? string.Empty))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion ?? string.Empty));
        }
    }
}
