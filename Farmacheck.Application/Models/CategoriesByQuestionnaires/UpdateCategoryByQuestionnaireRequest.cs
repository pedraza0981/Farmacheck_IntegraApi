using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Application.Models.CategoriesByQuestionnaires
{
    public class UpdateCategoryByQuestionnaireRequest : CategoryByQuestionnaireRequest
    {
        [Required]
        public byte Id { get; set; }
        public bool Activa { get; set; }
    }
}
