using Farmacheck.Application.Models.ChecklistSections;
using Farmacheck.Application.Models.GenericResponse;

namespace Farmacheck.Application.Interfaces
{
    public interface IChecklistSectionApiClient
    {
        Task<ChecklistSectionResponse> GetSectionsByChecklistAsync(int? id);

        Task<QuestionsBySectionResponse> GetQuestionsBySectionAsync(int cuestionarioId, int seccionId);

        Task<QuestionsBySectionResponse> GetSectionByNameAsync(int cuestionarioId, string nombre);

        Task DeleteAsync(RemoveChecklistSectionRequest removeRequest);

        Task<RegisterResponse> CreateAsync(ChecklistSectionRequest request);

        Task<UpdateResponse> UpdateAsync(UpdateChecklistSectionRequest request);

        Task<ChecklistSectionResponse?> GetSectionAsync(int cuestionarioId, int seccionId);
    }
}
