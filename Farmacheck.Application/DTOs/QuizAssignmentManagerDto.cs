namespace Farmacheck.Application.DTOs
{
    public class QuizAssignmentManagerDto
    {
        public int CuestionarioId { get; set; }
        public List<int> AsignacionPorSupervisor { get; set; } = new();
        public List<int> AsignacionDeAuditados { get; set; } = new();
        public List<int> AsignacionPorAuditor { get; set; } = new();
        public int? AsignadoPor { get; set; }
    }
}
