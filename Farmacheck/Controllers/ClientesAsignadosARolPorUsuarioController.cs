using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.DTOs;
using Farmacheck.Models;

namespace Farmacheck.Controllers
{
    public class ClientesAsignadosARolPorUsuarioController : Controller
    {
        private readonly IAssignedClientsByUserRoleApiClient _apiClient;
        private readonly IMapper _mapper;

        public ClientesAsignadosARolPorUsuarioController(IAssignedClientsByUserRoleApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int rolPorUsuarioId)
        {
            var apiData = await _apiClient.GetByUserRoleAsync(rolPorUsuarioId);
            if (apiData == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<AssignedClientsByUserRoleDto>(apiData);
            var model = _mapper.Map<ClientesAsignadosARolPorUsuarioViewModel>(dto);
            return Json(new { success = true, data = model });
        }
    }
}
