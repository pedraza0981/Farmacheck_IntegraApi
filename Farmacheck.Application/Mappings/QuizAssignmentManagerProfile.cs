using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.QuizAssignmentManager;

namespace Farmacheck.Application.Mappings
{
    public class QuizAssignmentManagerProfile : Profile
    {
        public QuizAssignmentManagerProfile()
        {
            CreateMap<QuizAssignmentManagerResponse, QuizAssignmentManagerDto>().ReverseMap();
            CreateMap<QuizAssignmentManagerRequest, QuizAssignmentManagerDto>().ReverseMap();
        }
    }
}
