namespace Farmacheck.Application.DTOs
{
    public class OpcionesPorPreguntaDto
    {
        public string Etiqueta { get; set; } = null!;

        public int Posicion { get; set; }

        public bool RequiereInformacionExtra { get; set; }

        public decimal? Ponderacion { get; set; }

        public bool RequiereEvidencia { get; set; }

        public bool GeneraTarea { get; set; }

        public bool Estatus { get; set; }

        public List<OpcionesComplementoPorPreguntaDto>? OpcionesComplemento { get; set; }
    }
}
