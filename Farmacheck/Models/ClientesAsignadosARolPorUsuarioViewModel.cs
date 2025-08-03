namespace Farmacheck.Models
{
    public class ClientesAsignadosARolPorUsuarioViewModel
    {
        public int RolPorUsuarioId { get; set; }
        public string RolNombre { get; set; } = null!;
        public string UnidadDeNegocioNombre { get; set; } = null!;
        public int TotalClientes { get; set; }
    }
}
