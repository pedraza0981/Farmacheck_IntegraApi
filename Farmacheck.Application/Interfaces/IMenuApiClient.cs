using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.Menus;

namespace Farmacheck.Application.Interfaces
{
    public interface IMenuApiClient
    {
        Task<IEnumerable<MenuResponse>> GetMenusAsync();
        Task<IEnumerable<MenuResponse>> GetVisibleMenusAsync();
        Task<IEnumerable<MenuResponse>> GetMenusByParentAsync(int? parentId);
        Task<PaginatedResponse<MenuResponse>> GetMenusByPageAsync(int page, int items);
        Task<MenuResponse?> GetMenuByIdAsync(int id);
        Task<int> CreateMenuAsync(MenuRequest request);
        Task<bool> UpdateMenuAsync(UpdateMenuRequest request);
        Task<bool> DeleteMenuAsync(int id);
    }
}
