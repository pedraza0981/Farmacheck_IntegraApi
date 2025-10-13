using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.Models.Calendario
{
    public sealed class UpsertCalendarResponse
    {
        public int CalendarioId { get; set; }

        public bool WasInserted { get; set; }

        public bool WasUpdated { get; set; }

        /// <summary>1 = INSERT, 2 = UPDATE</summary>
        public byte OperationCode { get; set; }

        public string OperationName => OperationCode switch
        {
            1 => "INSERT",
            2 => "UPDATE",
            _ => "UNKNOWN"
        };
    }
}
