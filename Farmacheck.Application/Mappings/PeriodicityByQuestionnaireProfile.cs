using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;

namespace Farmacheck.Application.Mappings
{
    public class PeriodicityByQuestionnaireProfile : Profile
    {
        public PeriodicityByQuestionnaireProfile()
        {
            CreateMap<PeriodicityByQuestionnaireResponse, PeriodicityByQuestionnaireDto>();
            CreateMap<PeriodicityByQuestionnaireDto, PeriodicityByQuestionnaireRequest>();
            CreateMap<PeriodicityByQuestionnaireDto, UpdatePeriodicityByQuestionnaireRequest>();
        }
    }
}
