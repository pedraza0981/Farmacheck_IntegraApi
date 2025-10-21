using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.BusinessStructures;
using Farmacheck.Application.Models.BusinessUnits;
using Farmacheck.Application.Models.Categories;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistScoreRating;
using Farmacheck.Application.Models.ChecklistSections;
using Farmacheck.Application.Models.Customers;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;
using Farmacheck.Application.Models.Roles;
using Farmacheck.Application.Models.HierarchyByRoles;
using Farmacheck.Application.Models.OptionsByQuestion;
using Farmacheck.Application.Models.OptionsComplementByQuestion;
using Farmacheck.Application.Models.Questions;
using Farmacheck.Application.Models.ResponseFormatByQuestion;
using Farmacheck.Application.Models.SubBrands;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Zones;
using Farmacheck.Application.Models.QuizAssignmentManager;
using Farmacheck.Models;
using Farmacheck.Models.Inputs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Farmacheck.Application.Models.LabelsByNumericalScale;
using Farmacheck.Application.Models.GroupingTags;
using Farmacheck.Application.Models.Sprints;
using Farmacheck.Application.Models.Tasks;
using Farmacheck.Application.Models.MailingProgramacion;
using Farmacheck.Application.Models.ZonaHorario;
using Farmacheck.Application.Models.NotificationCenter;
using Farmacheck.Application.Models.Menus;
using Farmacheck.Application.Models.RolMenus;

namespace Farmacheck.Helpers
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<MarcaDto, MarcaViewModel>();

            CreateMap<CategoryResponse, CategoriaDto>();
            CreateMap<ChecklistSummary, ChecklistSummaryDto>();
            CreateMap<ChecklistSummaryDto, ChecklistSummaryViewModel>();
            CreateMap<CategoryResponse, CategoriaViewModel>()
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus ?? false));
            CreateMap<CategoriaViewModel, CategoryRequest>();
            CreateMap<CategoriaViewModel, UpdateCategoryRequest>()
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => (bool?)src.Estatus));

            //CreateMap<MarcaViewModel, BrandRequest>();
            //CreateMap<MarcaViewModel, UpdateBrandRequest>();
            CreateMap<CuestionarioDto, CuestionarioViewModel>();

            CreateMap<SeccionDelCuestionarioDto, SeccionDelCuestionarioViewModel>();
            CreateMap<SeccionDto, SeccionViewModel>();

            CreateMap<ClasificacionDePuntajeDto, ClasificacionDePuntajeViewModel>();
            CreateMap<EtiquetasPorEscalaNumericaDto, EtiquetasPorEscalaNumericaViewModel>();

            CreateMap<BusinessUnitDto, UnidadDeNegocio>()
            .ForMember(dest => dest.LogotipoNombreArchivo, opt => opt.MapFrom(src => src.ArchivoImagen))
            .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen))
            .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia ?? src.Logotipo))
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.ImagenDeReferencia ?? src.Logotipo ?? string.Empty))
            .ForMember(dest => dest.Rfc, opt => opt.MapFrom(src => src.Rfc ?? string.Empty))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion ?? string.Empty));


            CreateMap<SubmarcaDto, SubMarca>();
            CreateMap<SubMarca, SubbrandRequest>();
            CreateMap<SubMarca, UpdateSubbrandRequest>();

            CreateMap<MarcaViewModel, UpdateBrandRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia))
                .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));


            CreateMap<MarcaViewModel, BrandRequest>()
            .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.UnidadDeNegocioId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia))
            .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen))
            .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));

            CreateMap<MenuDto, MenuViewModel>().ReverseMap();
            CreateMap<MenuViewModel, MenuRequest>();
            CreateMap<MenuViewModel, UpdateMenuRequest>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => (bool?)src.Activo))
                .ForMember(dest => dest.Visible, opt => opt.MapFrom(src => (bool?)src.Visible));

            CreateMap<RolMenuDto, RolMenuViewModel>().ReverseMap();
            CreateMap<RolMenuViewModel, RolMenuRequest>();
            CreateMap<RolMenuViewModel, UpdateRolMenuRequest>();
            CreateMap<RolMenuUsuarioDto, RolMenuUsuarioViewModel>();

            CreateMap<CuestionarioViewModel, ChecklistRequest>();
            CreateMap<CuestionarioViewModel, UpdateChecklistRequest>();

            CreateMap<ClasificacionDePuntajeViewModel, ChecklistScoreRatingRequest>();
            CreateMap<ClasificacionDePuntajeViewModel, UpdateChecklistScoreRatingRequest>();
            CreateMap<SeccionInputModel, ChecklistSectionRequest>()
                .ForMember(dest => dest.CuestionarioId, opt => opt.MapFrom(src => src.FormularioId))
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.CategoriaId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<PreguntaViewModel, QuestionRequest>()
                .ForMember(dest => dest.SeccionId, opt => opt.MapFrom(src => src.SeccionDelCuestionarioId));
            CreateMap<FormatoDeRespuestaPorPreguntaViewModel, ResponseFormatByQuestionRequest>();
            CreateMap<OpcionesPorPreguntaViewModel, OptionsByQuestionRequest>();
            CreateMap<OpcionesComplementoPorPreguntaViewModel, OptionsComplementByQuestionRequest>();

            CreateMap<FormatoDeRespuestaCatDto, FormatoDeRespuestaCatViewModel>();
            CreateMap<CategoriaDto, CategoriaViewModel>();

            CreateMap<EtiquetaDeAgrupacionDto, EtiquetaDeAgrupacionViewModel>();
            CreateMap<EtiquetaDeAgrupacionViewModel, UpdateGroupingTagRequest>();
            CreateMap<EtiquetaDeAgrupacionViewModel, GroupingTagRequest>();

            CreateMap<PreguntaDto, PreguntaViewModel>();
            CreateMap<FormatoDeRespuestaPorPreguntaDto, FormatoDeRespuestaPorPreguntaViewModel>();

            CreateMap<OpcionesPorPreguntaDto, OpcionesPorPreguntaViewModel>();
            CreateMap<OpcionesComplementoPorPreguntaDto, OpcionesComplementoPorPreguntaViewModel>();

            CreateMap<PreguntaViewModel, UpdateQuestionRequest>()
                .ForMember(dest => dest.SeccionId, opt => opt.MapFrom(src => src.SeccionDelCuestionarioId))
                .ForMember(dest => dest.PreguntaId, opt => opt.MapFrom(src => src.Id));

            CreateMap<FormatoDeRespuestaPorPreguntaViewModel, UpdateResponseFormatByQuestionRequest>();
            CreateMap<OpcionesPorPreguntaViewModel, UpdateOptionsByQuestionRequest>();
            CreateMap<OpcionesComplementoPorPreguntaViewModel, UpdateOptionsComplementByQuestionRequest>();
            
            CreateMap<EtiquetasPorEscalaNumericaViewModel, LabelsByNumericalScaleRequest>();
            CreateMap<EtiquetasPorEscalaNumericaViewModel, UpdateLabelsByNumericalScaleRequest>();

            CreateMap<SprintDto, SprintViewModel>();
            CreateMap<SprintViewModel, SprintRequest>();
            CreateMap<SprintViewModel, UpdateSprintRequest>();
            CreateMap<SprintSupervisorViewModel, SprintSupervisorRequest>();
            CreateMap<SprintRevisorViewModel, SprintRevisorRequest>();


            CreateMap<TareaDto, TareaViewModel>();
            CreateMap<TareaViewModel, TaskRequest>();
            CreateMap<TareaViewModel, UpdateTaskRequest>();

            CreateMap<PrioridadDeTareaDto, PrioridadDeTareaViewModel>();
            CreateMap<OrigenDeTareaDto, OrigenDeTareaViewModel>();
            CreateMap<OrigenDeTareaDto, OrigenDeTareaViewModel>();

            CreateMap<CustomerDto, ClienteEstructuraViewModel>()
             .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.UnidadDeNegocioId, opt => opt.MapFrom(src => src.BusinessStructure != null && src.BusinessStructure.Any() ? src.BusinessStructure.First().UnidadDeNegocioId : (int?)null))
             .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.BusinessStructure != null && src.BusinessStructure.Any() ? src.BusinessStructure.First().MarcaId : (int?)null))
             .ForMember(dest => dest.SubmarcaId, opt => opt.MapFrom(src => src.BusinessStructure != null && src.BusinessStructure.Any() ? src.BusinessStructure.First().SubmarcaId : (int?)null))
             .ForMember(dest => dest.ZonaId, opt => opt.MapFrom(src => src.BusinessStructure != null && src.BusinessStructure.Any() ? src.BusinessStructure.First().ZonaId : (int?)null))
             .ForMember(dest => dest.LatitudGPS, opt => opt.MapFrom(src => (decimal?)src.LatitudGps))
             .ForMember(dest => dest.LongitudGPS, opt => opt.MapFrom(src => (decimal?)src.LongitudGps))
             .ForMember(dest => dest.ModificadoEl, opt => opt.MapFrom(src => src.ModificadoEl))
             .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus))
             .ForMember(dest => dest.RadioGPS, opt => opt.MapFrom(src => (int?)src.RadioGps));

            CreateMap<ClienteEstructuraViewModel, CustomerRequest>()
                .ForMember(dest => dest.LatitudGps, opt => opt.MapFrom(src => src.LatitudGPS ?? 0))
                .ForMember(dest => dest.LongitudGps, opt => opt.MapFrom(src => src.LongitudGPS ?? 0))
                .ForMember(dest => dest.RadioGps, opt => opt.MapFrom(src => (short)(src.RadioGPS ?? 0)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));

            CreateMap<ClienteEstructuraViewModel, UpdateCustomerRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus))
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
                .ForMember(dest => dest.Estatus, opt => opt.MapFrom(src => src.Estatus));


            CreateMap<UnidadDeNegocio, BusinessUnitRequest>()
            .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo))
            .ForMember(dest => dest.ImagenDeReferencia, opt => opt.MapFrom(src => src.ImagenDeReferencia ?? src.Logotipo))
            .ForMember(dest => dest.ArchivoImagen, opt => opt.MapFrom(src => src.ArchivoImagen ?? src.LogotipoNombreArchivo))
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

            CreateMap<RoleResponse, RoleDto>()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb))
                .ReverseMap()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb));

            CreateMap<RoleDto, RolViewModel>()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb))
                .ReverseMap()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb));

            CreateMap<RolViewModel, RoleRequest>()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb));

            CreateMap<RolViewModel, UpdateRoleRequest>()
                .ForMember(dest => dest.AccesoWeb, opt => opt.MapFrom(src => src.AccesoWeb));

            CreateMap<CategoryByQuestionnaireResponse, CategoryByQuestionnaireDto>().ReverseMap();

            CreateMap<CategoryByQuestionnaireDto, CategoriaCuestionarioViewModel>().ReverseMap();

            CreateMap<CategoriaCuestionarioViewModel, CategoryByQuestionnaireRequest>();

            CreateMap<CategoriaCuestionarioViewModel, UpdateCategoryByQuestionnaireRequest>();

            CreateMap<PeriodicityByQuestionnaireResponse, PeriodicityByQuestionnaireDto>().ReverseMap();

            CreateMap<PeriodicityByQuestionnaireDto, PeriodicidadCuestionarioViewModel>()
                .ForMember(dest => dest.Meta, opt => opt.MapFrom(src => src.CadaCuantosDias))
                .ReverseMap()
                .ForMember(dest => dest.CadaCuantosDias, opt => opt.MapFrom(src => src.Meta));

            CreateMap<PeriodicidadCuestionarioViewModel, PeriodicityByQuestionnaireRequest>()
                .ForMember(dest => dest.CadaCuantosDias, opt => opt.MapFrom(src => src.Meta));

            CreateMap<PeriodicidadCuestionarioViewModel, UpdatePeriodicityByQuestionnaireRequest>()
                .ForMember(dest => dest.CadaCuantosDias, opt => opt.MapFrom(src => src.Meta));

            CreateMap<HierarchyByRoleDto, JerarquiaViewModel>().ReverseMap();
            CreateMap<JerarquiaViewModel, HierarchyByRoleRequest>();
            CreateMap<JerarquiaViewModel, UpdateHierarchyByRoleRequest>();

            CreateMap<UserDto, UsuarioViewModel>().ReverseMap();
            CreateMap<UsuarioViewModel, UserRequest>();
            CreateMap<UsuarioViewModel, UpdateUserRequest>();
            CreateMap<RelUserByRoleDto, UsuarioRolViewModel>().ReverseMap();
            CreateMap<UsuarioRolViewModel, UserByRoleRequest>();
            CreateMap<UsuarioRolViewModel, UpdateUserByRoleRequest>();
            CreateMap<RolPorUsuarioClientesAsignadosDto, RolPorUsuarioClientesAsignadosViewModel>().ReverseMap();
            CreateMap<QuizAssignmentManagerDto, AsignacionCuestionarioViewModel>().ReverseMap();
            CreateMap<AsignacionCuestionarioViewModel, QuizAssignmentManagerRequest>();
            CreateMap<UsuarioPorRolDto, UsuarioPorRolViewModel>().ReverseMap();

            CreateMap<vMailingProgramacionDestinatarioModel, vMailingProgramacionWebRequest>().ReverseMap();
            CreateMap<ZonaHorarioModel, ZonaHorarioRequest>().ReverseMap();



            CreateMap<TokenDto, TokenViewModel>().ReverseMap();
            CreateMap<UserInfoDto, UserInfoViewModel>().ReverseMap();

            CreateMap<NotificationTypeDto, NotificationTypeViewModel>().ReverseMap();
            CreateMap<NotificationDto, NotificationViewModel>().ReverseMap();
            CreateMap<NotificationFormatDto, NotificationFormatViewModel>().ReverseMap();
            CreateMap<NotificationSettingPostViewModel, NotificationSettingRequest>().ReverseMap();
            CreateMap<NotificationSettingViewModel, NotificationSetting>().ReverseMap();
        }
    }
}
