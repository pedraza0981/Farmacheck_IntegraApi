using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacheck.Models
{
    public class MailingProgramacionDestinatarioModel
    {
        public long Id { get; set; }

        public long Programacion_id { get; set; }

        public int? Usuario_id { get; set; }

        public string Email { get; set; }

    }
}
