using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios;

namespace Farmacheck.Application.Mappings
{
    public class ClientesAsignadosArolPorUsuariosProfile : Profile
    {
        public ClientesAsignadosArolPorUsuariosProfile()
        {
            CreateMap<RolPorUsuarioClientesAsignadosResponse, RolPorUsuarioClientesAsignadosDto>().ReverseMap();
        }
    }
}
