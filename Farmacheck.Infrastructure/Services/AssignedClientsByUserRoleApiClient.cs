using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.AssignedClientsByUserRole;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class AssignedClientsByUserRoleApiClient : IAssignedClientsByUserRoleApiClient
    {
        private readonly HttpClient _http;

        public AssignedClientsByUserRoleApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<AssignedClientsByUserRoleResponse?> GetByUserRoleAsync(int userRoleId)
        {
            return await _http.GetFromJsonAsync<AssignedClientsByUserRoleResponse>($"api/v1/ClientesAsignadosARolPorUsuario/{userRoleId}");
        }
    }
}
