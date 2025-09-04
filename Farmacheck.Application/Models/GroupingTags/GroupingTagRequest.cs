namespace Farmacheck.Application.Models.GroupingTags
{
    public class GroupingTagRequest
    {
        public int SeccionId { get; set; }

        public int CuestionarioId { get; set; }

        public string Etiqueta { get; set; } = null!;
    }
}
