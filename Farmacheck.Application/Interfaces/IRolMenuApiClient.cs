using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.RolMenus;

namespace Farmacheck.Application.Interfaces
{
    public interface IRolMenuApiClient
    {
        Task<IEnumerable<RolMenuResponse>> GetRolMenusAsync();

        Task<PaginatedResponse<RolMenuResponse>> GetRolMenusByPageAsync(int page, int items);

        Task<RolMenuResponse?> GetRolMenuByIdAsync(int id);

        Task<IEnumerable<RolMenuResponse>> GetRolMenusByRolAsync(int rolId);

        Task<IEnumerable<RolMenuUsuarioResponse>> GetRolMenusByUsuarioAsync(int usuarioId);

        Task<int> CreateRolMenuAsync(RolMenuRequest request);

        Task<bool> UpdateRolMenuAsync(UpdateRolMenuRequest request);

        Task<bool> DeleteRolMenuAsync(int id);
    }
}
