namespace Farmacheck.Application.Models.ChecklistScoreRating
{
    public class ChecklistScoreRatingResponse
    {
        public int PuntajeMaximo { get; set; }

        public string Etiqueta { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
