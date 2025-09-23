using Farmacheck.Application.Models.Categories;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface ICategoryApiClient
    {
        Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();

        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();

        Task<PaginatedResponse<CategoryResponse>> GetCategoriesByPageAsync(int page, int items);

        Task<CategoryResponse?> GetCategoryAsync(int id);

        Task<IEnumerable<CategoryResponse>> GetCategoriesByRoleAsync(int roleId);

        Task<int> CreateAsync(CategoryRequest request);

        Task<bool> UpdateAsync(UpdateCategoryRequest request);

        Task<bool> DeleteAsync(int id);

        Task<string> GetReportAsync();
    }
}
