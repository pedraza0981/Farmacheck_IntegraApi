using System.Collections.Generic;

namespace Farmacheck.Models.Inputs
{
    public class AsignacionFormularioInputModel
    {
        public int FormularioId { get; set; }
        public List<int> Rol1 { get; set; } = new();
        public int? Rol2 { get; set; }
        public List<int> Rol3 { get; set; } = new();
    }
}
