using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Farmacheck.Helpers
{
    public static class CatalogosStaticos
    {
        public static readonly List<SelectListItem> TiposPregunta = new()
        {
            new SelectListItem { Value = "7", Text = "Escala Cinco Estrellas" },
            new SelectListItem { Value = "5", Text = "Escala Facial" },
            new SelectListItem { Value = "4", Text = "Escala Numérica" },
            new SelectListItem { Value = "8", Text = "Fecha" },
            new SelectListItem { Value = "9", Text = "Horario" },
            new SelectListItem { Value = "11", Text = "Numérico" },
            new SelectListItem { Value = "1", Text = "Opción Múltiple" },
            new SelectListItem { Value = "10", Text = "Lista de Items" },
            new SelectListItem { Value = "6", Text = "Pulgar Arriba y Abajo" },
            new SelectListItem { Value = "2", Text = "Texto Corto" },
            new SelectListItem { Value = "3", Text = "Texto Largo" }
        };

        public static readonly List<SelectListItem> PreguntasPorEscala = new()
        {
            new SelectListItem { Value = "7", Text = "Escala Cinco Estrellas" },
            new SelectListItem { Value = "5", Text = "Escala Facial" },
            new SelectListItem { Value = "4", Text = "Escala Numérica" },
            new SelectListItem { Value = "6", Text = "Pulgar Arriba y Abajo" }
        };

        public static readonly List<SelectListItem> Prioridades = new()
        {
            new SelectListItem { Value = "1", Text = "Alta" },
            new SelectListItem { Value = "2", Text = "Media" },
            new SelectListItem { Value = "3", Text = "Baja" }
        };
    }
}
