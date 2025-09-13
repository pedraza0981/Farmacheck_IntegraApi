using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.ZonaHorario;

namespace Farmacheck.Application.Mappings
{
    public class ZonaHorarioProfile :Profile
    {
        public ZonaHorarioProfile()
        {
            CreateMap<ZonaHorarioResponse, ZonaHorariaDto>().ReverseMap();
        }
        
    }
}
