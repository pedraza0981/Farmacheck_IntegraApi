namespace Farmacheck.Models
{
    public class SubMarca
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; }

        public MarcaViewModel Marca { get; set; }
    }
}
