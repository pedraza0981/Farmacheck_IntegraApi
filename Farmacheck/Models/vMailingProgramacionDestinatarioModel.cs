using System.ComponentModel.DataAnnotations;

namespace Farmacheck.Models
{
    public class vMailingProgramacionDestinatarioModel
    {
        public long ProgramacionId { get; set; }

        public long ProgramacionDestinatarioId { get; set; }

        public int? UsuarioId { get; set; }

        public string Email { get; set; }

        public string UsuarioNombre { get; set; }
    }
}
