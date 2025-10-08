namespace Farmacheck.Application.Models.NotificationCenter;

public class NotificationResponse
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public long IdTipoNotificacion { get; set; }

    public IEnumerable<NotificationFormatResponse>? FormatosNotificaciones { get; set; }
}
