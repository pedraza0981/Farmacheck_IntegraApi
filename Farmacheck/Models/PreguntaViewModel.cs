using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Models
{
    public class PreguntaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; }

        public string? Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de pregunta válido.")]
        public int TipoPreguntaId { get; set; } // Relación a catálogo de tipos de pregunta
        public bool EsRequerido { get; set; }

        public int PrioridadId { get; set; } // Relación a catálogo de prioridades

        public string? Hipervinculo { get; set; }

        public bool AgregarComentario { get; set; }

        public bool AgregarImagen { get; set; }

        public bool AgregarCamposExtras { get; set; }

        // Relación con sección
        public int SeccionId { get; set; }

        // Lista de opciones con puntuación (solo si aplica)
        public List<OpcionValorViewModel> Opciones { get; set; } = new();

        public string? TipoPregunta { get; set; }


    }
}
