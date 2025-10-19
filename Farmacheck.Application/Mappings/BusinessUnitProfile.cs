using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.BusinessUnits;

namespace Farmacheck.Application.Mappings
{
    public class BusinessUnitProfile : Profile
    {
        public BusinessUnitProfile()
        {
            CreateMap<BusinessUnitResponse, BusinessUnitDto>();
            //CreateMap<BusinessUnitDto, BusinessUnitRequest>();
            CreateMap<BusinessUnitDto, BusinessUnitRequest>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.Rfc))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo ?? src.ImagenDeReferencia))
            .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia ?? src.Logotipo))
            .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen))
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));
        }
    }
}
