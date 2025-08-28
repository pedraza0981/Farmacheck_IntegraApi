using Farmacheck.Application.DTOs;

namespace Farmacheck.Models
{
    public class SeccionDelCuestionarioViewModel
    {
        public int Id { get; set; }

        public List<SeccionDto> Secciones { get; set; } = new();
    }
}
