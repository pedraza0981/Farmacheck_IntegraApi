namespace Farmacheck.Application.Models.Permissions
{
    public class PermissionResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estatus { get; set; }
        public DateTime CreadoEl { get; set; }
    }
}
