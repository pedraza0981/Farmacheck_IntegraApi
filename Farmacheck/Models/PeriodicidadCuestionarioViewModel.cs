namespace Farmacheck.Models
{
    public class PeriodicidadCuestionarioViewModel
    {
        public int CuestionarioId { get; set; }
        public string CuestionarioNombre { get; set; }
        public int Frecuencia { get; set; }
        public int Meta { get; set; }
        public string FrecuenciaDescripcion { get; set; }
    }
}
