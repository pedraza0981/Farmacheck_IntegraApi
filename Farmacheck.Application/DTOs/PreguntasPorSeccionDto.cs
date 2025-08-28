namespace Farmacheck.Application.DTOs
{
    public class PreguntasPorSeccionDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool Estatus { get; set; }

        public List<PreguntaDto>? Preguntas { get; set; }
    }
}
