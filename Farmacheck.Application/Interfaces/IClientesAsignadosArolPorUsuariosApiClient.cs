using Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios;
using Farmacheck.Application.Models.Common;

namespace Farmacheck.Application.Interfaces
{
    public interface IClientesAsignadosArolPorUsuariosApiClient
    {
        Task<List<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosAsync();
        Task<PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosByPageAsync(int page, int items);
        Task<ClientesAsignadosArolPorUsuarioResponse?> GetClienteAsignadoAsync(int id);
        Task<List<RolPorUsuarioClientesAsignadosResponse>> GetCountByRolPorUsuarioAsync(List<int> rolPorUsuarioIds, int usuarioId);
        Task<int> CreateAsync(ClientesAsignadosArolPorUsuarioRequest request);
        Task<bool> UpdateAsync(UpdateClientesAsignadosArolPorUsuarioRequest request);
        Task DeleteAsync(int id);
    }
}
