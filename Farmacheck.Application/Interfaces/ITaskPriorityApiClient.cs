using Farmacheck.Application.Models.TaskPriorities;

namespace Farmacheck.Application.Interfaces
{
    public interface ITaskPriorityApiClient
    {
        Task<IEnumerable<TaskPriorityResponse>> GetTaskPrioritiesAsync();
    }
}
