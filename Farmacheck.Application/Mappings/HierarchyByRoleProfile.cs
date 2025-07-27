using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.HierarchyByRoles;

namespace Farmacheck.Application.Mappings
{
    public class HierarchyByRoleProfile : Profile
    {
        public HierarchyByRoleProfile()
        {
            CreateMap<HierarchyByRoleResponse, HierarchyByRoleDto>().ReverseMap();
        }
    }
}
