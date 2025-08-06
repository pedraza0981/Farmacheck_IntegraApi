using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Users;

namespace Farmacheck.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, UserDto>().ReverseMap();
            CreateMap<RelUserByRoleResponse, RelUserByRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleByUserId))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src => src.Nombre))
                .ReverseMap()
                .ForMember(dest => dest.RoleByUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RolId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.RolNombre));
        }
    }
}
