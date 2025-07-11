using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.Zones;

namespace Farmacheck.Application.Mappings
{
    public class ZoneProfile : Profile
    {
        public ZoneProfile()
        {
            CreateMap<ZoneResponse, ZonaDto>().ReverseMap();
        }
    }
}
