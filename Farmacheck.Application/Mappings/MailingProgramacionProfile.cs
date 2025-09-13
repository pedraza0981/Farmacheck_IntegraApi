using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.MailingProgramacion;
using AutoMapper;

namespace Farmacheck.Application.Mappings
{
    public class MailingProgramacionProfile : Profile
    {
        public MailingProgramacionProfile()
        {
            CreateMap<MailingProgramacionResponse, MailingProgramacionDto>().ReverseMap();
        }
        
    }
}
