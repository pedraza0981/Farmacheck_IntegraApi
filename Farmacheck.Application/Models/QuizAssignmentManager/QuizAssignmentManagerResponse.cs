namespace Farmacheck.Application.Models.QuizAssignmentManager
{
    public class QuizAssignmentManagerResponse
    {
        public int CuestionarioId { get; set; }
        public List<int> AsignacionPorSupervisor { get; set; } = new();
        public List<int> AsignacionDeAuditados { get; set; } = new();
        public List<int> AsignacionPorAuditor { get; set; } = new();
    }
}
