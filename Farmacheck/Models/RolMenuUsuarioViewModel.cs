namespace Farmacheck.Models
{
    public class RolMenuUsuarioViewModel
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public int MenuId { get; set; }

        public bool PuedeVer { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Ruta { get; set; }

        public string? Icono { get; set; }

        public int Orden { get; set; }

        public bool Activo { get; set; }

        public bool Visible { get; set; }

        public int? ParentId { get; set; }
    }
}
