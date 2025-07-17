namespace Farmacheck.Application.Models.CategoriesByQuestionnaires
{
    public class CategoryByQuestionnaireResponse
    {
        public byte Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activa { get; set; }
        public string? NombreDelArchivoConIcono { get; set; }
        public DateTime ModificadaEl { get; set; }
    }
}
