using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.GroupingTags;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class GroupingTagApiClient : IGroupingTagApiClient
    {
        private readonly HttpClient _http;

        public GroupingTagApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<GroupingTagResponse>> GetTagsBySection(int cuestionarioId, int seccionId)
        {
            return await _http.GetFromJsonAsync<IEnumerable<GroupingTagResponse>>($"api/v1/groupingtags/filters?checklistId={cuestionarioId}&sectionId={seccionId}")
                   ?? new List<GroupingTagResponse>();
        }

        public async Task<int> CreateAsync(GroupingTagRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/groupingtags", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateGroupingTagRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/groupingtags", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}