using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface ICategoryByQuestionnaireApiClient
    {
        Task<List<CategoryByQuestionnaireResponse>> GetCategoriesAsync();
        Task<PaginatedResponse<CategoryByQuestionnaireResponse>> GetCategoriesByPageAsync(int page, int items);
        Task<CategoryByQuestionnaireResponse?> GetCategoryAsync(byte id);
        Task<byte> CreateAsync(CategoryByQuestionnaireRequest request);
        Task<bool> UpdateAsync(UpdateCategoryByQuestionnaireRequest request);
        Task DeleteAsync(byte id);
    }
}
