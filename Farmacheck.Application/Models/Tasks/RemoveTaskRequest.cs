namespace Farmacheck.Application.Models.Tasks
{
    public class RemoveTaskRequest
    {
        public Guid Id { get; set; }

        public int SprintId { get; set; }
    }
}
