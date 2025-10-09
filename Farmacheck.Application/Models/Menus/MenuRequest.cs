namespace Farmacheck.Application.Models.Menus
{
    public class MenuRequest
    {
        public string Nombre { get; set; } = null!;

        public string? Ruta { get; set; }

        public string? Icono { get; set; }

        public int Orden { get; set; }

        public bool Activo { get; set; } = true;

        public bool Visible { get; set; } = true;

        public int? ParentId { get; set; }
    }
}
