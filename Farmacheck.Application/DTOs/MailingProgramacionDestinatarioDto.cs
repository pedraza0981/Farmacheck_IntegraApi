using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacheck.Application.DTOs
{
    public class MailingProgramacionDestinatarioDto
    {
        public long ProgramacionId { get; set; }

        public long ProgramacionDestinatarioId { get; set; }

        public int? UsuarioId { get; set; }

        public string Email { get; set; }

        public string UsuarioNombre { get; set; }
    }
}
