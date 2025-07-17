namespace Farmacheck.Application.DTOs
{
    public class CategoryByQuestionnaireDto
    {
        public byte Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activa { get; set; }
        public string? NombreDelArchivoConIcono { get; set; }
        public DateTime ModificadaEl { get; set; }
    }
}
