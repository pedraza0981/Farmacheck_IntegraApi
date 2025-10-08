namespace Farmacheck.Application.Models.Tasks
{
    public class TaskRequest
    {
        public Guid Id { get; set; }

        public int SprintId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int PrioridadId { get; set; }

        public int CategoriaId { get; set; }

        public int OrigenId { get; set; }

        public DateTime VigenteDel { get; set; }

        public DateTime VenceEl { get; set; }

        public bool RequiereEvidencia { get; set; }

        public string ComentarioDeReferencia { get; set; } = null!;

        public List<int> Clientes { get; set; } = new List<int>();

        public List<int> Roles { get; set; } = new List<int>();
    }
}
