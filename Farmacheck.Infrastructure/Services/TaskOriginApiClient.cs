using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.TaskOrigins;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class TaskOriginApiClient : ITaskOriginApiClient
    {
        private readonly HttpClient _http;

        public TaskOriginApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<TaskOriginResponse>> GetTaskOriginsAsync()
        {
            var origins = await _http.GetFromJsonAsync<IEnumerable<TaskOriginResponse>>("api/v1/taskorigins")
                   ?? Enumerable.Empty<TaskOriginResponse>();

            return origins;
        }
    }
}