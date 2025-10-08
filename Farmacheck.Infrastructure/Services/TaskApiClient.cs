using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Sprints;
using Farmacheck.Application.Models.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Farmacheck.Infrastructure.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public TaskApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddBearerToken()
        {
            if (_http.DefaultRequestHeaders.Authorization != null)
            {
                return;
            }

            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<TaskResponse>> GetTasksAsync(int sprintId)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<TaskResponse>>($"api/v1/tasks/filters?sprintId={sprintId}")
                ?? new List<TaskResponse>();
        }

        public async Task<TaskResponse> GetTaskAsync(int sprintId, Guid? id)
        {
            AddBearerToken();
            var url = $"api/v1/tasks/filters?sprintId={sprintId}&taskId={id.ToString()}";
            var result = await _http.GetFromJsonAsync<List<TaskResponse>>(url);

            if (result is null)
                return new TaskResponse();

            return result.Any() ? result.SingleOrDefault() : new TaskResponse();
        }

        public async Task<Guid> CreateAsync(TaskRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/tasks", request);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<Guid>();
            return id;
        }

        public async Task<bool> UpdateAsync(UpdateTaskRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/tasks", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(RemoveTaskRequest removeRequest)
        {
            AddBearerToken();

            string jsonContent = JsonConvert.SerializeObject(removeRequest);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/v1/tasks");
            request.Content = content;
            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetReport()
        {
            AddBearerToken();
            var response = await _http.GetAsync("api/v1/tasks/report");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
