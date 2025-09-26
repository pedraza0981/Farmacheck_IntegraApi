using Farmacheck.Application.DTOs;

namespace Farmacheck.Models
{
    public class ChecklistSummaryViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public List<SeccionViewModel>? Secciones { get; set; }
    }
}
