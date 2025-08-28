using Farmacheck.Application.Models.ChecklistSections;

namespace Farmacheck.Application.Interfaces
{
    public interface IChecklistSectionApiClient
    {
        Task<ChecklistSectionResponse> GetSectionsByChecklistAsync(int? id);

        Task<QuestionsBySectionResponse> GetQuestionsBySectionAsync(int cuestionarioId, int seccionId);

        Task DeleteAsync(RemoveChecklistSectionRequest removeRequest);

        Task<int> CreateAsync(ChecklistSectionRequest request);

        Task<bool> UpdateAsync(UpdateChecklistSectionRequest request);

        Task<ChecklistSectionResponse?> GetSectionAsync(int cuestionarioId, int seccionId);
    }
}
