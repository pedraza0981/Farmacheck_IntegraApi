using Farmacheck.Application.Models.OptionsComplementByQuestion;

namespace Farmacheck.Application.Models.OptionsByQuestion
{
    public class OptionsByQuestionRequest
    {
        public string Etiqueta { get; set; } = null!;

        public bool RequiereEvidencia { get; set; }

        public bool RequiereInformacionExtra { get; set; }

        public bool GeneraTarea { get; set; }

        public decimal? Ponderacion { get; set; }

        public IEnumerable<OptionsComplementByQuestionRequest>? OpcionesComplemento { get; set; }
    }
}
