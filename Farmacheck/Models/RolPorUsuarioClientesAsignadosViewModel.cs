namespace Farmacheck.Models
{
    public class RolPorUsuarioClientesAsignadosViewModel
    {
        public int RolPorUsuarioId { get; set; }
        public int RolId { get; set; }
        public string RoleNombre { get; set; } = string.Empty;
        public int UnidadDeNegocioId { get; set; }
        public string UnidadDeNegocioNombre { get; set; } = string.Empty;
        public int TotalClientesAsignados { get; set; }
    }
}
