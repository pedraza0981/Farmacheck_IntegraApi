using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class AddEventoCalendarResponse
    {
        public int? EventoId { get; set; }

        public bool Success { get; set; }

        public int? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
