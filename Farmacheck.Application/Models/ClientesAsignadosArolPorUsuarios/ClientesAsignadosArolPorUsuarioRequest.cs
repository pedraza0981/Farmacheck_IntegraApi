namespace Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios
{
    public class ClientesAsignadosArolPorUsuarioRequest
    {
        public int RolPorUsuarioId { get; set; }
        public long ClienteId { get; set; }
        public bool GeolocalizacionActiva { get; set; }
    }
}
