using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.MailingProgramacion;

namespace Farmacheck.Application.Mappings
{
    public class MailingProgramacionDestinatarioProfile : Profile
    {
        public MailingProgramacionDestinatarioProfile()
        {
            CreateMap<MailingProgramacionDestinatarioResponse, MailingProgramacionDestinatarioDto>()
            .ForMember(d => d.ProgramacionDestinatarioId, opt => opt.MapFrom(s => s.ProgramacionDestinatarioId))
            .ForMember(d => d.UsuarioNombre, opt => opt.NullSubstitute(string.Empty))
            .ForMember(d => d.UsuarioId, opt => opt.MapFrom(s => s.UsuarioId))
            .ForMember(d => d.ProgramacionId, opt => opt.MapFrom(s => s.ProgramacionId))

            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));
        }

    }
}
