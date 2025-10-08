namespace Farmacheck.Models
{
    public class TareaViewModel
    {
        public Guid? Id { get; set; }

        public int SprintId { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int PrioridadId { get; set; }

        public int CategoriaId { get; set; }

        public int? OrigenId { get; set; }

        public string VigenciaInicio { get; set; } = string.Empty;

        public string VigenciaFin { get; set; } = string.Empty;

        public DateTime? VigenteDel { get; set; }

        public DateTime? VenceEl { get; set; }

        public bool RequiereEvidencia { get; set; }

        public string ComentarioDeReferencia { get; set; } = null!;

        public bool? Estatus { get; set; }

        public IEnumerable<int>? Clientes { get; set; }

        public IEnumerable<int>? Roles { get; set; }
    }
}
