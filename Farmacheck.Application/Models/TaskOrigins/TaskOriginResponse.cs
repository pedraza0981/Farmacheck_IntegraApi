namespace Farmacheck.Application.Models.TaskOrigins
{
    public class TaskOriginResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Estatus { get; set; }
    }
}
