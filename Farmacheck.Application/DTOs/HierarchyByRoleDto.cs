namespace Farmacheck.Application.DTOs
{
    public class HierarchyByRoleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int RolSuperiorId { get; set; }
        public int RolSubordinadoId { get; set; }
        public DateTime AsignadoEl { get; set; }
        public bool Estatus { get; set; }
    }
}
