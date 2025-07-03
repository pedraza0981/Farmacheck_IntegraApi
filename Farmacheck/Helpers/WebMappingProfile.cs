using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Models;

namespace Farmacheck.Helpers
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<MarcaDto, MarcaViewModel>();
        }
    }
}
