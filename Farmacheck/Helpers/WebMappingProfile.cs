using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.BusinessUnits;
using Farmacheck.Models;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.SubBrands;
using Farmacheck.Application.Models.Customers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Farmacheck.Application.Models.BusinessStructures;
using Farmacheck.Application.Models.Zones;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Roles;

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

            CreateMap<MarcaViewModel, UpdateBrandRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo));
                //.ForMember(dest => dest.LogotipoNombreArchivo, opt => opt.MapFrom(src => src.LogotipoNombreArchivo));


            CreateMap<MarcaViewModel, BrandRequest>()
            .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo));
            //.ForMember(dest => dest.LogotipoNombreArchivo, opt => opt.MapFrom(src => src.LogotipoNombreArchivo));

            CreateMap<CustomerDto, ClienteEstructuraViewModel>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.BusinessStructure.UnidadDeNegocioId))
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.BusinessStructure.MarcaId))
                .ForMember(dest => dest.SubmarcaId, opt => opt.MapFrom(src => src.BusinessStructure.SubmarcaId))
                .ForMember(dest => dest.ZonaId, opt => opt.MapFrom(src => src.BusinessStructure.ZonaId))
                .ForMember(dest => dest.LatitudGPS, opt => opt.MapFrom(src => (int?)src.LatitudGps))
                .ForMember(dest => dest.LongitudGPS, opt => opt.MapFrom(src => (int?)src.LongitudGps))
                .ForMember(dest => dest.ModificadoEl, opt => opt.MapFrom(src => src.ModificadoEl))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus ? 1 : 0))
                .ForMember(dest => dest.RadioGPS, opt => opt.MapFrom(src => (int?)src.RadioGps));

            CreateMap<ClienteEstructuraViewModel, CustomerRequest>()
                .ForMember(dest => dest.LatitudGps, opt => opt.MapFrom(src => src.LatitudGPS ?? 0))
                .ForMember(dest => dest.LongitudGps, opt => opt.MapFrom(src => src.LongitudGPS ?? 0))
                .ForMember(dest => dest.RadioGps, opt => opt.MapFrom(src => (short)(src.RadioGPS ?? 0)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClienteId));

            CreateMap<ClienteEstructuraViewModel, UpdateCustomerRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus == 1))
                .ForMember(dest => dest.LatitudGps, opt => opt.MapFrom(src => src.LatitudGPS ?? 0))
                .ForMember(dest => dest.LongitudGps, opt => opt.MapFrom(src => src.LongitudGPS ?? 0))
                .ForMember(dest => dest.RadioGps, opt => opt.MapFrom(src => (short)(src.RadioGPS ?? 0)));

            CreateMap<ClienteEstructuraViewModel, BusinessStructureRequest>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.MarcaId ?? 0))
                .ForMember(dest => dest.SubmarcaId, opt => opt.MapFrom(src => src.SubmarcaId))
                .ForMember(dest => dest.ZonaId, opt => opt.MapFrom(src => src.ZonaId ?? 0));

            CreateMap<ClienteEstructuraViewModel, UpdateBusinessStructureRequest>()
                .IncludeBase<ClienteEstructuraViewModel, BusinessStructureRequest>()
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus == 1));


            CreateMap<UnidadDeNegocio, BusinessUnitRequest>()
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo ?? string.Empty))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.Rfc ?? string.Empty))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion ?? string.Empty));

            CreateMap<CustomerTypeDto, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<ZonaDto, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Nombre));


            CreateMap<ZoneResponse, ZonaDto>().ReverseMap();
                        
            CreateMap<ZonaDto, ZonaViewModel>().ReverseMap();
                        
            CreateMap<ZonaViewModel, ZoneRequest>();
            
            CreateMap<ZonaViewModel, UpdateZoneRequest>();

            CreateMap<RoleResponse, RoleDto>().ReverseMap();
            CreateMap<RoleDto, RolViewModel>().ReverseMap();
            CreateMap<RolViewModel, RoleRequest>();
            CreateMap<RolViewModel, UpdateRoleRequest>();

            CreateMap<CategoryByQuestionnaireResponse, CategoryByQuestionnaireDto>().ReverseMap();

            CreateMap<CategoryByQuestionnaireDto, CategoriaCuestionarioViewModel>().ReverseMap();

            CreateMap<CategoriaCuestionarioViewModel, CategoryByQuestionnaireRequest>();

            CreateMap<CategoriaCuestionarioViewModel, UpdateCategoryByQuestionnaireRequest>();
        }
    }
}
