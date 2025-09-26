namespace Farmacheck.Models
{
    public class FormatoDeRespuestaPorPreguntaViewModel
    {
        public int FormatoId { get; set; }

        public string FormatoNombre { get; set; } = string.Empty;

        public bool PermiteMultipleSeleccion { get; set; }
    }
}
