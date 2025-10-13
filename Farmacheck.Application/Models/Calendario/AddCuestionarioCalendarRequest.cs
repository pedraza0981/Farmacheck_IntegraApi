using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class AddCuestionarioCalendarRequest
    {
        public AddCuestionarioCalendarRequest() { }

        public int CalendarioId { get; set; }

        public int CuestionarioId { get; set; }

        public bool? Activo { get; set; } = null; // default SP = 1

        public bool? PermiteMultiplesResp { get; set; } = null; // default SP = 0

        public bool? PermiteDescripcion { get; set; } = null; // default SP = 0

        public string? Descripcion { get; set; }

        public bool? TodoElDiaDefault { get; set; } = null; // default SP = 0

        public DateTime? FechaInicioUtc { get; set; }

        public DateTime? FechaFinUtc { get; set; }

        public string? RRuleDefault { get; set; }

        public int CreadoPorUsuarioId { get; set; }

        public string? ColorHex { get; set; } // default SP = '#1E90FF'
    }
}
