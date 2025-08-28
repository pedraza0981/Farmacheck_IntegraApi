namespace Farmacheck.Application.DTOs
{
    public class ClasificacionDePuntajeDto
    {
        public int PuntajeMaximo { get; set; }

        public string Etiqueta { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
