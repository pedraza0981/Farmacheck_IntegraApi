using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Permissions;

namespace Farmacheck.Application.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionResponse, PermissionDto>().ReverseMap();
        }
    }
}
