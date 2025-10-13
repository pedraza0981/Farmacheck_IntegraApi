using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class ConfiguracionCalendarioResponse
    {
        public int Id { get; set; }

        public int? CalendarioId { get; set; }

        public int UsuarioId { get; set; }

        public string ColorHex { get; set; }

        public bool UsarColorPersonal { get; set; }

        public DateTime CreadoEn { get; set; }

        public DateTime ActualizadoEn { get; set; }

        public string VistaPorDefecto { get; set; }

        public int PrimerDiaSemana { get; set; }

        public TimeSpan HoraLaboralInicio { get; set; }

        public TimeSpan HoraLaboralFin { get; set; }

        public List<ConfiguracionCalendarNotifPayloadResponse> Notificaciones { get; set; }
    }
}
