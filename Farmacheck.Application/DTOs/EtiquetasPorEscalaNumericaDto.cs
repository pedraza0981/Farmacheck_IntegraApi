namespace Farmacheck.Application.DTOs
{
    public class EtiquetasPorEscalaNumericaDto
    {
        public int Id { get; set; }

        public int LimiteInferior { get; set; }

        public int LimiteSuperior { get; set; }

        public string EtiquetaParaEscalaInferior { get; set; } = null!;

        public string EtiquetaParaEscalaSuperior { get; set; } = null!;

        public bool Estatus { get; set; }
    }
}
