using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Roles;

namespace Farmacheck.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, RoleDto>().ReverseMap();
        }
    }
}
