namespace Farmacheck.Models
{
    public class CategoriaCuestionarioViewModel
    {
        public byte Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activa { get; set; }
        public string? NombreDelArchivoConIcono { get; set; }
        public DateTime ModificadaEl { get; set; }
    }
}
