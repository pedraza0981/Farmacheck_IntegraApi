using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.MailingProgramacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Mappings
{
    public class TipoReporteProfile : Profile
    {
        public TipoReporteProfile()
        {
            CreateMap<TipoReporteResponse, TipoReporteDto>().ReverseMap();
        }
    }
}
