using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.Checklists;

namespace Farmacheck.Application.Interfaces
{
    public interface IChecklistApiClient
    {
        Task<IEnumerable<ChecklistResponse>> GetAllChecklistsAsync();

        Task DeleteAsync(int id);

        Task<int> CreateAsync(ChecklistRequest request);

        Task<bool> UpdateAsync(UpdateChecklistRequest request);

        Task<ChecklistResponse?> GetChecklistAsync(int? id);

        Task<string> GetReport(int checklistId);
    }
}
