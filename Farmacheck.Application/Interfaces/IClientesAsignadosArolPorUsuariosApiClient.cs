using Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios;

namespace Farmacheck.Application.Interfaces
{
    public interface IClientesAsignadosArolPorUsuariosApiClient
    {
        Task<List<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosAsync();
        Task<List<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosByPageAsync(int page, int items);
        Task<ClientesAsignadosArolPorUsuarioResponse?> GetClienteAsignadoAsync(int id);
        Task<List<RolPorUsuarioClientesAsignadosResponse>> GetCountByRolPorUsuarioAsync(List<int> rolPorUsuarioIds, int usuarioId);
        Task<int> CreateAsync(ClientesAsignadosArolPorUsuarioRequest request);
        Task<bool> UpdateAsync(UpdateClientesAsignadosArolPorUsuarioRequest request);
        Task DeleteAsync(int id);
    }
}
