using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Models
{
    public class CuestionarioViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del formulario")]
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public byte CategoriaId { get; set; }

        public string NombreDelArchivoConLaImagen { get; set; } = null!;

        public string Archivo { get; set; } = null!;

        public bool Estatus { get; set; }

        [Display(Name = "Alias del formulario")]
        public string Alias { get; set; } = null!;

        // Propiedades
        public bool NotificacionesPorCorreoAlFinalizarRevision { get; set; }

        public string ListadoDeCorreosParaNotifiacionAlFinalizarAdicionales { get; set; } = null!;

        public bool SustituirLogoEnPDF { get; set; }

        public bool SustituirLogoEnEmpresa { get; set; }

        public bool ActivarGeolocalizacionEnCuestionario { get; set; }

        public bool EsconderCalificacion { get; set; }

        public bool OcultarTareasPendientes { get; set; }

        public bool PersonalizarFirmasAdicionales { get; set; }

        public string EtiquetaDeFirma1 { get; set; } = null!;

        public string EtiquetaDeFirma2 { get; set; } = null!;

        public bool PublicarCuestionario { get; set; }
        
        public DateTime CreadoEl { get; set; }

        public int? RangoVerde { get; set; }

        public int? RangoAmarillo { get; set; }

        public int? RangoRojo { get; set; }

        public List<ClasificacionDePuntajeViewModel> ClasificacionesDePuntaje { get; } = new();

        // Asignación
        public string Auditor { get; set; }
        public string Auditado { get; set; }
        public string Supervisor { get; set; }
        public string UnidadNegocio { get; set; }

        public IEnumerable<SelectListItem> AuditoresDisponibles { get; set; }
        public IEnumerable<SelectListItem> AuditadosDisponibles { get; set; }
        public IEnumerable<SelectListItem> SupervisoresDisponibles { get; set; }
        public IEnumerable<SelectListItem> UnidadesDisponibles { get; set; }
    }
}
