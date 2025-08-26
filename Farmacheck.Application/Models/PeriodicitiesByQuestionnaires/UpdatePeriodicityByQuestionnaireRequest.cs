using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.PeriodicitiesByQuestionnaires
{
    public class UpdatePeriodicityByQuestionnaireRequest : PeriodicityByQuestionnaireRequest
    {
        [Required]
        public new int CuestionarioId { get; set; }
    }
}
