namespace Farmacheck.Application.Models.Questions
{
    public class RemoveQuestionRequest
    {
        public int Id { get; set; }
        
        public int SeccionDelCuestionarioId { get; set; }

        public int CuestionarioId { get; set; }
    }
}
