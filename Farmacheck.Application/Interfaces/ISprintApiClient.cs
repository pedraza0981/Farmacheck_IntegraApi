using Farmacheck.Application.Models.Sprints;

namespace Farmacheck.Application.Interfaces
{
    public interface ISprintApiClient
    {
        Task<List<SprintResponse>> GetSprintsAsync();

        Task<SprintResponse?> GetSprintAsync(int? id);

        Task<int> CreateAsync(SprintRequest request);

        Task<bool> UpdateAsync(UpdateSprintRequest request);

        Task DeleteAsync(int id);

        Task<string> GetReport();
    }
}
