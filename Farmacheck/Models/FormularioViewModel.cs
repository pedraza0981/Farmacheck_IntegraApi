using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Models
{
    public class FormularioViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del formulario")]
        public string Nombre { get; set; }

        [Display(Name = "Alias del formulario")]
        public string Alias { get; set; }

        [Display(Name = "Etiqueta de campo")]
        public string EtiquetaCampo { get; set; }

        [Display(Name = "Estándar de la Compañía")]
        public int EstandarCompania { get; set; }

        // Asignación
        public string Auditor { get; set; }
        public string Auditado { get; set; }
        public string Supervisor { get; set; }
        public string UnidadNegocio { get; set; }

        public IEnumerable<SelectListItem> AuditoresDisponibles { get; set; }
        public IEnumerable<SelectListItem> AuditadosDisponibles { get; set; }
        public IEnumerable<SelectListItem> SupervisoresDisponibles { get; set; }
        public IEnumerable<SelectListItem> UnidadesDisponibles { get; set; }

        // Firma
        public bool FirmaObligatoria { get; set; }
        public bool PersonalizarFirmas { get; set; }
        public string Etiqueta1 { get; set; }
        public string Etiqueta2 { get; set; }

        // Notificaciones
        public bool NotifCorreoFinRevision { get; set; }
        public bool NotifCorreoBajoEstandar { get; set; }
        public bool NotifPushBajoEstandar { get; set; }
        public string CorreosAdicionales { get; set; }
        public string PushAdicionales { get; set; }

        // PDF
        public bool SustituirLogoPDF { get; set; }
        public bool SustituirEmpresaPDF { get; set; }

        // Extras
        public string PlanesAccionAutomatico { get; set; }
        public string Categoria { get; set; }
        public bool CaracterInformativo { get; set; }
        public bool OcultarTareasPendientes { get; set; }
        public bool PermitirFotosGaleria { get; set; }
        public bool ActivarGeolocalizacion { get; set; }
        public bool Publicar { get; set; }
        public bool EsconderCalificacion { get; set; }
        public string Rutinas { get; set; }
        // Datos generales
        public DateTime Fecha { get; set; }
        public bool EstaActivo { get; set; }
    }
}
