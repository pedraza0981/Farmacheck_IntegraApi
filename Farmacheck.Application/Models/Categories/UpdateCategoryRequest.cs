namespace Farmacheck.Application.Models.Categories
{
    public class UpdateCategoryRequest : CategoryRequest
    {
        public int Id { get; set; }

        public bool? Estatus { get; set; }
    }
}
