using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.CustomersRolesUsers;

namespace Farmacheck.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserApiClient _apiClient;
        private readonly IBrandApiClient _brandApi;
        private readonly IMapper _mapper;
        private readonly IBusinessUnitApiClient _businessUnitApi;
        private readonly ISubbrandApiClient _subbrandApi;
        private readonly IZoneApiClient _zoneApi;
        private readonly IClientesAsignadosArolPorUsuariosApiClient _clientesAsignadosArolPorUsuariosApiClient;
        private readonly ICustomersApiClient _customersApi;
        private readonly IUserByRoleApiClient _userByRoleApiClient;
        private readonly ICustomersRolesUsersApiClient _customersRolesUsersApiClient;  

        public UsuarioController(IUserApiClient apiClient, IBrandApiClient brandApi, IMapper mapper, IBusinessUnitApiClient businessUnitApi, 
                                 ISubbrandApiClient subbrandApi, IZoneApiClient zoneApi,IClientesAsignadosArolPorUsuariosApiClient clientesAsignadosArolPorUsuariosApiClient, 
                                 ICustomersApiClient customersApi, IUserByRoleApiClient userByRoleApiClient, ICustomersRolesUsersApiClient customersRolesUsersApiClient)
        {
            _apiClient = apiClient;
            _brandApi = brandApi;
            _mapper = mapper;
            _businessUnitApi = businessUnitApi;
            _subbrandApi = subbrandApi;
            _zoneApi = zoneApi;
            _clientesAsignadosArolPorUsuariosApiClient = clientesAsignadosArolPorUsuariosApiClient;
            _customersApi = customersApi;
            _userByRoleApiClient = userByRoleApiClient;
            _customersRolesUsersApiClient = customersRolesUsersApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetUsersAsync();
            var dtos = _mapper.Map<List<UserDto>>(apiData);
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(dtos);

            return View(usuarios);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetUsersAsync();
            var dtos = _mapper.Map<List<UserDto>>(apiData);
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(dtos);

            return Json(new { success = true, data = usuarios });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            var entidad = await _apiClient.GetUserAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<UserDto>(entidad);
            var model = _mapper.Map<UsuarioViewModel>(dto);
            return Json(new { success = true, data = model });
        }

        

        [HttpGet]
        public async Task<JsonResult> ListarUnidadesNegocio()
        {
            var apiData = await _businessUnitApi.GetBusinessUnitsAsync();
            var dtos = _mapper.Map<List<BusinessUnitDto>>(apiData);
            var unidades = _mapper.Map<List<UnidadDeNegocio>>(dtos);

            return Json(new { success = true, data = unidades });
        }
        [HttpGet]
        public async Task<JsonResult> ListarMarcas(int unidadId)
        {
            var apiData = await _brandApi.GetBrandsByBusinessUnitAsync(unidadId);
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var marcas = _mapper.Map<List<MarcaViewModel>>(dtos);

            return Json(new { success = true, data = marcas });
        }
        [HttpGet]
        public async Task<JsonResult> ListarPorMarca(List<int> marcaId)
        {
            var apiData = await _subbrandApi.GetSubbrandsByBrandsAsync( marcaId );
            var dtos = _mapper.Map<List<SubmarcaDto>>(apiData);
            var submarcas = _mapper.Map<List<SubMarca>>(dtos);

            return Json(new { success = true, data = submarcas });
        }

        [HttpGet]
        public async Task<JsonResult> ListarZonas()
        {
            var apiData = await _zoneApi.GetZonesAsync();
            var dtos = _mapper.Map<List<ZonaDto>>(apiData);
            var zonas = _mapper.Map<List<ZonaViewModel>>(dtos);

            return Json(new { success = true, data = zonas });
        }

        [HttpGet]
        public async Task<JsonResult> ListarClientesPorFiltros(List<int> subbrandIds, List<int> zoneIds)
        {
            var apiData = await _customersApi.GetCustomersByFiltersAsync(subbrandIds, zoneIds);
            var dtos = _mapper.Map<List<CustomerDto>>(apiData);
            var clientes = _mapper.Map<List<ClienteEstructuraViewModel>>(dtos);

            return Json(new { success = true, data = clientes });
        }

        [HttpPost]
        public async Task<JsonResult> GuardarRolPorUsuario([FromBody] UsuarioRolViewModel model)
        {
            int userRoleId = 0;

            try
            {
                
                var request = _mapper.Map<UserByRoleRequest>(model);
                userRoleId = await _userByRoleApiClient.CreateAsync(request);

                if (userRoleId <= 0)
                {
                    return Json(new { success = false, error = "No se pudo crear el rol del usuario." });
                }
                                
                var customerRolUserRequest = new CustomerRolUserRequest
                {
                    RolPorUsuarioId = userRoleId,
                    Clientes = model.ClienteIds ?? new List<long>(),
                    GeolocalizacionActiva = true
                };

                var result = await _customersRolesUsersApiClient.CreateAsync(customerRolUserRequest);


                if (userRoleId <= 0)
                {                    
                    return Json(new { success = false, error = "No se pudo asociar clientes al rol del usuario." });
                }

                return Json(new { success = true, id = userRoleId });
            }
            catch (Exception ex)
            {                
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }


        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] UsuarioViewModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre))
                    return Json(new { success = false, error = "El nombre es obligatorio." });

                var request = _mapper.Map<UserRequest>(model);

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updateRequest = _mapper.Map<UpdateUserRequest>(model);
                    var updated = await _apiClient.UpdateAsync(updateRequest);
                    if (updated)
                        return Json(new { success = true, id = model.Id });

                    return Json(new { success = false, error = "No se pudo actualizar" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<JsonResult> ListarRolesPorUsuario(int usuarioId)
        {
            var apiData = await _userByRoleApiClient.GetByUserAsync(usuarioId);
            var dtos = _mapper.Map<List<UserByRoleDto>>(apiData);
            var roles = _mapper.Map<List<UsuarioRolViewModel>>(dtos);

            return Json(new { success = true, data = roles });
        }

        [HttpGet]
        public async Task<JsonResult> ListarRolPorUsuarioCount(List<int> rolPorUsuarioIds, int usuarioId)
        {
            var apiData = await _clientesAsignadosArolPorUsuariosApiClient.GetCountByRolPorUsuarioAsync(rolPorUsuarioIds, usuarioId);
            var dtos = _mapper.Map<List<RolPorUsuarioClientesAsignadosDto>>(apiData);
            var counts = _mapper.Map<List<RolPorUsuarioClientesAsignadosViewModel>>(dtos);

            return Json(new { success = true, data = counts });
        }

        
    }
}
