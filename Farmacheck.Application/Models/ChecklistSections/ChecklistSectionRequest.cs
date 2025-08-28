namespace Farmacheck.Application.Models.ChecklistSections
{
    public class ChecklistSectionRequest
    {
        public int CuestionarioId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
