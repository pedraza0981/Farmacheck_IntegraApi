namespace Farmacheck.Models.Inputs
{
    public class SeccionInputModel
    {        
        public int Id { get; set; }
        public int FormularioId { get; set; }
        public string Titulo { get; set; }
        public bool ActivarNA { get; set; }
        public string Descripcion { get; set; }
        public string RangoVerde { get; set; }
        public string RangoAmarillo { get; set; }
        public string RangoRojo { get; set; }
    }
}
