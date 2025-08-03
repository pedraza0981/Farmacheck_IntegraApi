using System;

namespace Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios
{
    public class RolPorUsuarioClientesAsignadosResponse
    {
        public int RolPorUsuarioId { get; set; }
        public int RolId { get; set; }
        public string RoleNombre { get; set; } = string.Empty;
        public int UnidadDeNegocioId { get; set; }
        public string UnidadDeNegocioNombre { get; set; } = string.Empty;
        public int TotalClientesAsignados { get; set; }
    }
}
