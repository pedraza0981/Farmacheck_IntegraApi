using System;

namespace Farmacheck.Application.DTOs
{
    public class SubmarcaDto
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? Estatus { get; set; }
        public DateTime? ModificadaEl { get; set; }
    }
}
