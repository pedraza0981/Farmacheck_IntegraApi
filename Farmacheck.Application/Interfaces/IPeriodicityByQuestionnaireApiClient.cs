using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.PeriodicitiesByQuestionnaires;

namespace Farmacheck.Application.Interfaces
{
    public interface IPeriodicityByQuestionnaireApiClient
    {
        Task<List<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesAsync();
        Task<PaginatedResponse<PeriodicityByQuestionnaireResponse>> GetPeriodicitiesByPageAsync(int page, int items);
        Task<PeriodicityByQuestionnaireResponse?> GetPeriodicityAsync(int id);
        Task<bool> CreateAsync(PeriodicityByQuestionnaireRequest request);
        Task<bool> UpdateAsync(UpdatePeriodicityByQuestionnaireRequest request);
        Task DeleteAsync(int id);
    }
}
