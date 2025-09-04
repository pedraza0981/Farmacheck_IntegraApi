namespace Farmacheck.Models
{
    public class EtiquetaDeAgrupacionViewModel
    {
        public int Id { get; set; }

        public int SeccionId { get; set; }

        public int CuestionarioId { get; set; }

        public string Etiqueta { get; set; } = null!;
    }
}
