namespace Farmacheck.Models.Request
{
    public class ConfiguracionCalendarNotifPayloadRequest
    {
        public string Seccion { get; set; } = "";
        public string Canal { get; set; } = "";     // push | email | sms
        public int Cantidad { get; set; }
        public string Unidad { get; set; } = "";     // minutes | hours | days
    }
}
