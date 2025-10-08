namespace Farmacheck.Models
{
    public class NotificationViewModel
    {
        public long Id { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public long IdTipoNotificacion { get; set; }

        public bool IsChecked { get; set; }

        public IEnumerable<NotificationFormatViewModel>? FormatosNotificaciones { get; set; }
    }
}
