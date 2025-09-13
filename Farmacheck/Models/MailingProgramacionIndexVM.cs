using Farmacheck.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Farmacheck.Models
{
    public class MailingProgramacionIndexVM
    {
        public List<vMailingProgramacionWebDto> Programaciones { get; set; } = new();
        public string Usuario { get; set; }
        public IEnumerable<SelectListItem> UsuariosDisponibles { get; set; }

    }
}
