using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Sprints;
using Farmacheck.Application.Models.TaskOrigins;
using Farmacheck.Application.Models.TaskPriorities;
using Farmacheck.Application.Models.Tasks;

namespace Farmacheck.Application.Mappings
{
    public class SprintProfile : Profile
    {
        public SprintProfile()
        {
            CreateMap<SprintResponse, SprintDto>();
            CreateMap<TaskResponse, TareaDto>();
            CreateMap<TaskPriorityResponse, PrioridadDeTareaDto>();
            CreateMap<TaskOriginResponse, OrigenDeTareaDto>();
        }
    }
}
