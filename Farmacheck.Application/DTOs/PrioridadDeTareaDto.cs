namespace Farmacheck.Application.DTOs
{
    public class PrioridadDeTareaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Estatus { get; set; }
    }
}
