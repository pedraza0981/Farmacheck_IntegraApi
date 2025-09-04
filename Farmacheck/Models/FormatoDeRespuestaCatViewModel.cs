namespace Farmacheck.Models
{
    public class FormatoDeRespuestaCatViewModel
    {
        public int Id { get; set; }

        public string Formato { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool BasadoEnEscalaNumerica { get; set; }

        public bool Estatus { get; set; }
    }
}
