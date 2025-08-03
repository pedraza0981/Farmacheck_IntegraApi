using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.AssignedClientsByUserRole;

namespace Farmacheck.Application.Mappings
{
    public class AssignedClientsByUserRoleProfile : Profile
    {
        public AssignedClientsByUserRoleProfile()
        {
            CreateMap<AssignedClientsByUserRoleResponse, AssignedClientsByUserRoleDto>().ReverseMap();
        }
    }
}
