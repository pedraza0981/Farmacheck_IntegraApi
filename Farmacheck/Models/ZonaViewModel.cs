namespace Farmacheck.Models
{
    public class ZonaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estatus { get; set; }
        public DateTime? ModificadaEl { get; set; }
    }
}
