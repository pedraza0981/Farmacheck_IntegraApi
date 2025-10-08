namespace Farmacheck.Application.DTOs
{
    public class UsuarioPorRolDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; } = null!;
        public DateTime AsignadoEl { get; set; }
        public int AsignadoPor { get; set; }
        public bool Estatus { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
    }
}
