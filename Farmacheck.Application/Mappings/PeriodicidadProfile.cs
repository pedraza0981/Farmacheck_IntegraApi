using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.MailingProgramacion;
using Farmacheck.Application.Models.ZonaHorario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Mappings
{
    public class PeriodicidadProfile : Profile
    {
        public PeriodicidadProfile()
        {
            CreateMap<PeriodicidadResponse, PeriodicidadDto>().ReverseMap();
        }
    }
}
