namespace Farmacheck.Models.Inputs
{
    public class SeccionInputModel
    {        
        public int Id { get; set; }
        public int FormularioId { get; set; }

        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
