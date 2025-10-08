namespace Farmacheck.Application.DTOs;

public class NotificationDto
{
    public long Id { get; set; }

    public string Descripcion { get; set; } = string.Empty;

    public long IdTipoNotificacion { get; set; }

    public IEnumerable<NotificationFormatDto>? FormatosNotificaciones { get; set; }
}
