using Farmacheck.Application.Models.Checklists;
using Farmacheck.Application.Models.ChecklistScoreRating;

namespace Farmacheck.Application.Models.Checklists
{
    public class UpdateChecklistRequest : Checklist
    {
        public List<UpdateChecklistScoreRatingRequest> ClasificacionDePuntaje { get; set; } = new();
    }
}
