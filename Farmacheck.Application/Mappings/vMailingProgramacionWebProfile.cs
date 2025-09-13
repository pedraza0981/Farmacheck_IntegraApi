using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.MailingProgramacion;
using AutoMapper;


namespace Farmacheck.Application.Mappings
{
    public class vMailingProgramacionWebProfile: Profile
    {
        public vMailingProgramacionWebProfile()
        {
            CreateMap<vMailingProgramacionWebResponse, vMailingProgramacionWebDto>().ReverseMap();
        }
    }
}
