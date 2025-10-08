using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.NotificationCenter;

public class NotificationTypeResponse
{
    public long Id { get; set; }

    public string TipoNotificacion { get; set; } = string.Empty;

    public IEnumerable<NotificationResponse>? Notificaciones { get; set; }
}
