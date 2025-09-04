using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.ResponseFormat;

namespace Farmacheck.Application.Mappings
{
    public class ResponseFormatCatProfile : Profile
    {
        public ResponseFormatCatProfile()
        {
            CreateMap<ResponseFormatCatResponse, FormatoDeRespuestaCatDto>();
        }
    }
}
