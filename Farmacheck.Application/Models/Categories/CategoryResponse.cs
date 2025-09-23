using System;

namespace Farmacheck.Application.Models.Categories
{
    public class CategoryResponse
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime? CreadaEl { get; set; }

        public DateTime? ModificadaEl { get; set; }

        public bool? Estatus { get; set; }
    }
}
