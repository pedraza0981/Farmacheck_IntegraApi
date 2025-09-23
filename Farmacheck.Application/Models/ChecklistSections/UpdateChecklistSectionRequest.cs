namespace Farmacheck.Application.Models.ChecklistSections
{
    public class UpdateChecklistSectionRequest
    {
        public int SeccionId { get; set; }

        public int CuestionarioId { get; set; }

        public string Nombre { get; set; } = null!;

        public int? CategoriaId { get; set; }
    }
}
