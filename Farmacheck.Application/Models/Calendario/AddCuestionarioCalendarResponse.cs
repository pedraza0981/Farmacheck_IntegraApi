using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public class AddCuestionarioCalendarResponse
    {
        public int? CalCueId { get; init; }
        public bool Success { get; init; }
        public int? ErrorCode { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
