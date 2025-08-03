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
            CreateMap<UserByRoleResponse, UserByRoleDto>().ReverseMap();
        }
    }
}
