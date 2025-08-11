using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;
using System.Linq;

namespace Farmacheck.Infrastructure.Services
{
    public class ClientesAsignadosArolPorUsuariosApiClient : IClientesAsignadosArolPorUsuariosApiClient
    {
        private readonly HttpClient _http;

        public ClientesAsignadosArolPorUsuariosApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosAsync()
        {
            return await _http.GetFromJsonAsync<List<ClientesAsignadosArolPorUsuarioResponse>>("api/v1/ClientesAsignadosArolPorUsuarios")
                   ?? new List<ClientesAsignadosArolPorUsuarioResponse>();
        }

        public async Task<PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosByPageAsync(int page, int items)
        {
            var url = $"api/v1/ClientesAsignadosArolPorUsuarios/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>>(url)
                      ?? new PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>();

            return res;
        }

        public async Task<ClientesAsignadosArolPorUsuarioResponse?> GetClienteAsignadoAsync(int id)
        {
            return await _http.GetFromJsonAsync<ClientesAsignadosArolPorUsuarioResponse>($"api/v1/ClientesAsignadosArolPorUsuarios/{id}");
        }

        public async Task<List<RolPorUsuarioClientesAsignadosResponse>> GetCountByRolPorUsuarioAsync(List<int> rolPorUsuarioIds, int usuarioId)
        {
            var query = string.Join("&", rolPorUsuarioIds.Select(id => $"rolPorUsuarioIds={id}"));
            var url = $"api/v1/ClientesAsignadosArolPorUsuarios/rolPorUsuario/usuario/{usuarioId}?{query}";
            return await _http.GetFromJsonAsync<List<RolPorUsuarioClientesAsignadosResponse>>(url)
                   ?? new List<RolPorUsuarioClientesAsignadosResponse>();
        }

        public async Task<int> CreateAsync(ClientesAsignadosArolPorUsuarioRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/ClientesAsignadosArolPorUsuarios", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateClientesAsignadosArolPorUsuarioRequest request)
        {
            var response = await _http.PutAsJsonAsync("api/v1/ClientesAsignadosArolPorUsuarios", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/v1/ClientesAsignadosArolPorUsuarios/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
