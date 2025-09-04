namespace Farmacheck.Application.Models.GroupingTags
{
    public class UpdateGroupingTagRequest
    {
        public int Id { get; set; }

        public int SeccionId { get; set; }

        public int CuestionarioId { get; set; }

        public string Etiqueta { get; set; } = null!;
    }
}
