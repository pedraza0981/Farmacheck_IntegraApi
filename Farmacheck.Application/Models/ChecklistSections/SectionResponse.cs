using Farmacheck.Application.Models.Questions;

namespace Farmacheck.Application.Models.ChecklistSections
{
    public class SectionResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int? CategoriaId { get; set; }

        public string CategoriaNombre { get; set; } = null!;

        public bool Estatus { get; set; }

        public IEnumerable<QuestionResponse>? Preguntas { get; set; }
    }
}
