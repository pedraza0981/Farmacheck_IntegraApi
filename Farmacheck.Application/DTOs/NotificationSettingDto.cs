namespace Farmacheck.Application.DTOs;

public class NotificationSettingDto
{
    public long Id { get; set; }

    public long IdNotificacion { get; set; }

    public long IdFormatoNotificacion { get; set; }

    public DateTime CreadaEl { get; set; } = DateTime.Now;

    public bool Estatus { get; set; } = true;
}
