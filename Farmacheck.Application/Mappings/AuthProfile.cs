using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Security;

namespace Farmacheck.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<TokenResponse, TokenDto>().ReverseMap();
        CreateMap<UserInfoResponse, UserInfoDto>().ReverseMap();
    }
}
