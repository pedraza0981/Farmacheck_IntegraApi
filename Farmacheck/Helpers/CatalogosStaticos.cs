using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Farmacheck.Helpers
{
    public static class CatalogosStaticos
    {
        public static readonly List<SelectListItem> TiposPregunta = new()
        {
            new SelectListItem { Value = "1", Text = "Opción Múltiple" },
            new SelectListItem { Value = "2", Text = "Respuesta Abierta" }
        };

        public static readonly List<SelectListItem> Prioridades = new()
        {
            new SelectListItem { Value = "1", Text = "Alta" },
            new SelectListItem { Value = "2", Text = "Media" },
            new SelectListItem { Value = "3", Text = "Baja" }
        };
    }
}
