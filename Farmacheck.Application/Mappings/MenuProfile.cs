using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Menus;

namespace Farmacheck.Application.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuResponse, MenuDto>().ReverseMap();
        }
    }
}
