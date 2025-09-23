using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Categories;
using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistScoreRating;
using Farmacheck.Application.Models.ChecklistSections;
using Farmacheck.Application.Models.GroupingTags;
using Farmacheck.Application.Models.LabelsByNumericalScale;
using Farmacheck.Application.Models.OptionsByQuestion;
using Farmacheck.Application.Models.OptionsComplementByQuestion;
using Farmacheck.Application.Models.Questions;
using Farmacheck.Application.Models.ResponseFormatByQuestion;

namespace Farmacheck.Application.Mappings
{
    public class ChecklistProfile : Profile
    {
        public ChecklistProfile()
        {
            CreateMap<ChecklistResponse, CuestionarioDto>();
            
            CreateMap<CuestionarioDto, ChecklistRequest>();
            CreateMap<ChecklistScoreRatingResponse, ClasificacionDePuntajeDto>();

            CreateMap<ChecklistSectionResponse, SeccionDelCuestionarioDto>();
            CreateMap<SectionResponse, SeccionDto>();
            CreateMap<GroupingTagResponse, EtiquetaDeAgrupacionDto>();

            CreateMap<QuestionsBySectionResponse, PreguntasPorSeccionDto>();
            CreateMap<QuestionResponse, PreguntaDto>();
            CreateMap<ResponseFormatByQuestionResponse, FormatoDeRespuestaPorPreguntaDto>();
            CreateMap<OptionsByQuestionResponse, OpcionesPorPreguntaDto>();
            CreateMap<OptionsComplementByQuestionResponse, OpcionesComplementoPorPreguntaDto>();
            CreateMap<LabelsByNumericalScaleResponse, EtiquetasPorEscalaNumericaDto>();
            CreateMap<CategoryResponse, CategoriaDto>();
        }
    }
}
