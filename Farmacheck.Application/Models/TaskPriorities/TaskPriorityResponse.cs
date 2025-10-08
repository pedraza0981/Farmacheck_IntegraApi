namespace Farmacheck.Application.Models.TaskPriorities
{
    public class TaskPriorityResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Estatus { get; set; }
    }
}
