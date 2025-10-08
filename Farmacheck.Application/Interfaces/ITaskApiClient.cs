using Farmacheck.Application.Models.Tasks;

namespace Farmacheck.Application.Interfaces
{
    public interface ITaskApiClient
    {
        Task<List<TaskResponse>> GetTasksAsync(int sprintId);

        Task<TaskResponse> GetTaskAsync(int sprintId, Guid? id);

        Task<Guid> CreateAsync(TaskRequest request);

        Task<bool> UpdateAsync(UpdateTaskRequest request);

        Task DeleteAsync(RemoveTaskRequest request);

        Task<string> GetReport();
    }
}
