using Farmacheck.Application.Models.TaskOrigins;

namespace Farmacheck.Application.Interfaces
{
    public interface ITaskOriginApiClient
    {
        Task<IEnumerable<TaskOriginResponse>> GetTaskOriginsAsync();
    }
}
