namespace Farmacheck.Infrastructure.Models.Zones
{
    public class UpdateZoneRequest : ZoneRequest
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }
    }
}
