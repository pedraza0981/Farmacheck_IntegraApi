using Farmacheck.Application.Models.OptionsComplementByQuestion;

namespace Farmacheck.Application.Models.OptionsByQuestion
{
    public class UpdateOptionsByQuestionRequest
    {
        public string Etiqueta { get; set; } = null!;

        public bool? RequiereEvidencia { get; set; }

        public bool? RequiereInformacionExtra { get; set; }

        public decimal? Ponderacion { get; set; }

        public bool? Estatus { get; set; }

        public IEnumerable<UpdateOptionsComplementByQuestionRequest>? OpcionesComplemento { get; set; }
    }
}
