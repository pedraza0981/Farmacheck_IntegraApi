namespace Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios
{
    public class UpdateClientesAsignadosArolPorUsuarioRequest : ClientesAsignadosArolPorUsuarioRequest
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }
        public new bool? GeolocalizacionActiva { get; set; }
    }
}
