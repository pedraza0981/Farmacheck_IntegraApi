using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.TaskPriorities;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class TaskPriorityApiClient : ITaskPriorityApiClient
    {
        private readonly HttpClient _http;

        public TaskPriorityApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<TaskPriorityResponse>> GetTaskPrioritiesAsync()
        {
            var formats = await _http.GetFromJsonAsync<IEnumerable<TaskPriorityResponse>>("api/v1/taskpriorities")
                   ?? Enumerable.Empty<TaskPriorityResponse>();

            return formats;
        }
    }
}

