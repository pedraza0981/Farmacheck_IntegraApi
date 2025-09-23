using Farmacheck.Application.Models.Categories;

namespace Farmacheck.Application.Interfaces
{
    public interface ICategoryApiClient
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
    }
}
