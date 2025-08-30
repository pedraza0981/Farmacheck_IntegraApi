using Farmacheck.Application.Models.QuizAssignmentManager;

namespace Farmacheck.Application.Interfaces
{
    public interface IQuizAssignmentManagerApiClient
    {
        Task<QuizAssignmentManagerResponse?> GetQuizAssignmentAsync(int questionaryId);
        Task<bool> CreateAsync(QuizAssignmentManagerRequest request);
        Task<bool> DeleteAsync(int questionaryId, List<int> asignacionPorSupervisor, List<int> asignacionDeAuditados, List<int> asignacionPorAuditor);
    }
}
