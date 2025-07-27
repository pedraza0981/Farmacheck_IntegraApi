namespace Farmacheck.Models
{
    public class JerarquiaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int RolSuperiorId { get; set; }
        public int RolSubordinadoId { get; set; }
        public string? RolSuperiorNombre { get; set; }
        public string? RolSubordinadoNombre { get; set; }
        public DateTime AsignadoEl { get; set; }
        public bool Estatus { get; set; }
    }
}
