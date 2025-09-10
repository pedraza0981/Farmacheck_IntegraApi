using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.ClientesAsignadosArolPorUsuarios;
using Farmacheck.Application.Models.Common;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Farmacheck.Infrastructure.Services
{
    public class ClientesAsignadosArolPorUsuariosApiClient : IClientesAsignadosArolPorUsuariosApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientesAsignadosArolPorUsuariosApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddBearerToken()
        {
            if (_http.DefaultRequestHeaders.Authorization != null)
            {
                return;
            }

            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosAsync()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<List<ClientesAsignadosArolPorUsuarioResponse>>("api/v1/ClientesAsignadosArolPorUsuarios")
                   ?? new List<ClientesAsignadosArolPorUsuarioResponse>();
        }

        public async Task<PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>> GetClientesAsignadosByPageAsync(int page, int items)
        {
            AddBearerToken();
            var url = $"api/v1/ClientesAsignadosArolPorUsuarios/pages?page={page}&items={items}";
            var res = await _http.GetFromJsonAsync<PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>>(url)
                      ?? new PaginatedResponse<ClientesAsignadosArolPorUsuarioResponse>();

            return res;
        }

        public async Task<ClientesAsignadosArolPorUsuarioResponse?> GetClienteAsignadoAsync(int id)
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<ClientesAsignadosArolPorUsuarioResponse>($"api/v1/ClientesAsignadosArolPorUsuarios/{id}");
        }

        public async Task<List<RolPorUsuarioClientesAsignadosResponse>> GetCountByRolPorUsuarioAsync(List<int> rolPorUsuarioIds, int usuarioId)
        {
            AddBearerToken();
            var query = string.Join("&", rolPorUsuarioIds.Select(id => $"rolPorUsuarioIds={id}"));
            var url = $"api/v1/ClientesAsignadosArolPorUsuarios/rolPorUsuario/usuario/{usuarioId}?{query}";
            return await _http.GetFromJsonAsync<List<RolPorUsuarioClientesAsignadosResponse>>(url)
                   ?? new List<RolPorUsuarioClientesAsignadosResponse>();
        }

        public async Task<int> CreateAsync(ClientesAsignadosArolPorUsuarioRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/ClientesAsignadosArolPorUsuarios", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task<bool> UpdateAsync(UpdateClientesAsignadosArolPorUsuarioRequest request)
        {
            AddBearerToken();
            var response = await _http.PutAsJsonAsync("api/v1/ClientesAsignadosArolPorUsuarios", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task DeleteAsync(int id)
        {
            AddBearerToken();
            var response = await _http.DeleteAsync($"api/v1/ClientesAsignadosArolPorUsuarios/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
