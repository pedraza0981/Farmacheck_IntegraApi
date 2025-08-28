namespace Farmacheck.Application.DTOs
{
    public class OpcionesComplementoPorPreguntaDto
    {
        public string Nombre { get; set; } = null!;

        public string ListaDeOpcionesPredefinidas { get; set; } = null!;

        public bool Estatus { get; set; }
    }
}
