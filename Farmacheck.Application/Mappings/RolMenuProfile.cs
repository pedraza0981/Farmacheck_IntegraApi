using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.RolMenus;

namespace Farmacheck.Application.Mappings
{
    public class RolMenuProfile : Profile
    {
        public RolMenuProfile()
        {
            CreateMap<RolMenuResponse, RolMenuDto>().ReverseMap();
            CreateMap<RolMenuUsuarioResponse, RolMenuUsuarioDto>().ReverseMap();
        }
    }
}
