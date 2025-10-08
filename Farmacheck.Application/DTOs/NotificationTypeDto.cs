namespace Farmacheck.Application.DTOs;

public class NotificationTypeDto
{
    public long Id { get; set; }

    public string TipoNotificacion { get; set; } = string.Empty;

    public IEnumerable<NotificationDto>? Notificaciones { get; set; }
}
