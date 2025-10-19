using System.Collections.Generic;

namespace Farmacheck.Models
{
    public class UnidadDeNegocio
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }
        public string Nombre { get; set; }
        public string? Logotipo { get; set; }

        public string? LogotipoNombreArchivo { get; set; }
        public string? ImagenDeReferencia { get; set; }
        public string? ArchivoImagen { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }

        //public ICollection<MarcaViewModel> Marcas { get; set; } = new List<MarcaViewModel>();
    }
}
