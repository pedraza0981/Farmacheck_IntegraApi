using Farmacheck.Application.Models.ChecklistScoreRating;
using Farmacheck.Application.Models.ChecklistSections;

namespace Farmacheck.Application.Models.Checklists
{
    public class ChecklistSummary
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<SectionResponse>? Secciones { get; set; }
    }
}
