using Farmacheck.Application.Models.Questions;

namespace Farmacheck.Application.Models.ChecklistSections
{
    public class QuestionsBySectionResponse
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public bool? Estatus { get; set; }

        public List<QuestionResponse>? Preguntas { get; set; }
    }
}
