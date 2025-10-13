using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class ConfiguracionCalendarNotifPayloadResponse
    {
        public string Seccion { get; set; } = "";
        public string Canal { get; set; } = "";     // push | email | sms
        public int Cantidad { get; set; }
        public string Unidad { get; set; } = "";     // minutes | hours | days
    }
}
