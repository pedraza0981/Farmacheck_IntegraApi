namespace Farmacheck.Application.DTOs
{
    public class SeccionDelCuestionarioDto
    {
        public int Id { get; set; }

        public List<SeccionDto> Secciones { get; set; } = new();
    }
}
