namespace Farmacheck.Application.DTOs
{
    public class ChecklistSummaryDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<SeccionDto>? Secciones { get; set; }
    }
}
