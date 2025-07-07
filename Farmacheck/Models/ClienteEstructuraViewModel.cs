namespace Farmacheck.Models
{
    public class ClienteEstructuraViewModel
    {
        // Datos del cliente
        public int ClienteId { get; set; }
        public int? UnidadDeNegocioId { get; set; }
        public string CentroDeCosto { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string NumeroDeTelefono { get; set; }
        public int? LatitudGPS { get; set; }
        public int? LongitudGPS { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public int Estatus { get; set; }
        public int? RadioGPS { get; set; }
        public int TipoDeClienteId { get; set; }
        
        public int? MarcaId { get; set; }
        public int? SubmarcaId { get; set; }
        public int? ZonaId { get; set; }

        
        //public string MarcaNombre { get; set; }
        //public string SubmarcaNombre { get; set; }
        public string ZonaNombre { get; set; }
    }

}
