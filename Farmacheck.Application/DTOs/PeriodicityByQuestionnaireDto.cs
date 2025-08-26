namespace Farmacheck.Application.DTOs
{
    public class PeriodicityByQuestionnaireDto
    {
        public int CuestionarioId { get; set; }
        public int Frecuencia { get; set; }
        public int CadaCuantosDias { get; set; }
    }
}
