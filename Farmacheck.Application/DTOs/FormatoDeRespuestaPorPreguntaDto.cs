namespace Farmacheck.Application.DTOs
{
    public class FormatoDeRespuestaPorPreguntaDto
    {
        public int FormatoId { get; set; }

        public string FormatoNombre { get; set; } = string.Empty;

        public bool PermiteMultipleSeleccion { get; set; }

        public bool Estatus { get; set; }
    }
}
