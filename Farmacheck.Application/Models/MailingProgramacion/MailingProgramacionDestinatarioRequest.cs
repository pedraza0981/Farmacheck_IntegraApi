
namespace Farmacheck.Application.Models.MailingProgramacion
{
    public class MailingProgramacionDestinatarioRequest
    {
        public long Id { get; set; }

        public long Programacion_id { get; set; }

        public int? Usuario_id { get; set; }

        public string Email { get; set; }

    }
}
