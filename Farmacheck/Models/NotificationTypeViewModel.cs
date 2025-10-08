namespace Farmacheck.Models
{
    public class NotificationTypeViewModel
    {
        public long Id { get; set; }

        public string TipoNotificacion { get; set; } = string.Empty;

        public IEnumerable<NotificationViewModel>? Notificaciones { get; set; }
    }
}
