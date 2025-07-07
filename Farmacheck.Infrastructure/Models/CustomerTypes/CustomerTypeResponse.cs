namespace Farmacheck.Infrastructure.Models.CustomerTypes
{
    public class CustomerTypeResponse
    {
        public short Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estatus { get; set; }
        public DateTime? ModificadoEl { get; set; }
    }
}
