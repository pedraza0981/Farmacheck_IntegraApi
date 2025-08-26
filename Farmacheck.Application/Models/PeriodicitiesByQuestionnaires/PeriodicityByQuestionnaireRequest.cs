namespace Farmacheck.Application.Models.PeriodicitiesByQuestionnaires
{
    public class PeriodicityByQuestionnaireRequest
    {
        public int CuestionarioId { get; set; }
        public int Frecuencia { get; set; }
        public int CadaCuantosDias { get; set; }
    }
}
