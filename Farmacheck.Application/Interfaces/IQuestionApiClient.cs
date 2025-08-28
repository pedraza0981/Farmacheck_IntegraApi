using Farmacheck.Application.Models.Questions;

namespace Farmacheck.Application.Interfaces
{
    public interface IQuestionApiClient
    {
        Task DeleteAsync(RemoveQuestionRequest removeRequest);

        Task<int> CreateAsync(QuestionRequest request);

        Task<bool> UpdateAsync(UpdateQuestionRequest request);

        Task<QuestionResponse?> GetQuestionAsync(int cuestionarioId, int seccionId, int questionId);
    }
}
