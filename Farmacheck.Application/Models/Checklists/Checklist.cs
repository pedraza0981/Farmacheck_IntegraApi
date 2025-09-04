namespace Farmacheck.Aplication.Models.Checklists
{
    public class Checklist
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public byte CategoriaId { get; set; }

        public string NombreDelArchivoConLaImagen { get; set; } = null!;

        public string ImagenArchivo { get; set; } = null!;

        public string Alias { get; set; } = null!;

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

        public bool Estatus { get; set; }
    }
}
