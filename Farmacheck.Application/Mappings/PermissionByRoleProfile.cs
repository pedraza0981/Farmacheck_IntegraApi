using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.PermissionsByRoles;

namespace Farmacheck.Application.Mappings
{
    public class PermissionByRoleProfile : Profile
    {
        public PermissionByRoleProfile()
        {
            CreateMap<PermissionByRoleResponse, PermissionByRoleDto>().ReverseMap();
        }
    }
}
