using Farmacheck.Application.Models.GroupingTags;
using static System.Net.WebRequestMethods;

namespace Farmacheck.Application.Interfaces
{
    public interface IGroupingTagApiClient
    {
        Task<IEnumerable<GroupingTagResponse>> GetTagsBySection(int cuestionarioId, int seccionId);

        Task<int> CreateAsync(GroupingTagRequest request);

        Task<bool> UpdateAsync(UpdateGroupingTagRequest request);
    }
}
