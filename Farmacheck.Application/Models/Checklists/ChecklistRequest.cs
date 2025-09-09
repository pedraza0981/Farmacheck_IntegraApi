using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistScoreRating;

namespace Farmacheck.Application.Models.Checklists
{
    public class ChecklistRequest : Checklist
    {
        public List<ChecklistScoreRatingRequest> ClasificacionDePuntaje { get; set; } = new();
    }
}
