using Farmacheck.Application.Models.OptionsComplementByQuestion;

namespace Farmacheck.Application.Models.OptionsByQuestion
{
    public class OptionsByQuestionResponse
    {
        public string Etiqueta { get; set; } = null!;

        public int Posicion { get; set; }

        public bool RequiereInformacionExtra { get; set; }

        public decimal Ponderacion { get; set; }

        public bool RequiereEvidencia { get; set; }

        public bool Estatus { get; set; }

        public IEnumerable<OptionsComplementByQuestionResponse>? OpcionesComplemento { get; set; }
    }
}
