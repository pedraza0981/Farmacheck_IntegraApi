using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;

namespace Farmacheck.Application.Mappings
{
    public class CategoryByQuestionnaireProfile : Profile
    {
        public CategoryByQuestionnaireProfile()
        {
            CreateMap<CategoryByQuestionnaireResponse, CategoryByQuestionnaireDto>();
            CreateMap<CategoryByQuestionnaireDto, CategoryByQuestionnaireRequest>();
            CreateMap<CategoryByQuestionnaireDto, UpdateCategoryByQuestionnaireRequest>();
        }
    }
}
