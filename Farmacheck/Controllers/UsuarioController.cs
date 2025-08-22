using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using Farmacheck.Application.Models.Common;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.CustomersRolesUsers;
using System.Linq;

namespace Farmacheck.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserApiClient _apiClient;
        private readonly IBrandApiClient _brandApi;
        private readonly IMapper _mapper;
        private readonly IBusinessUnitApiClient _businessUnitApi;
        private readonly ISubbrandApiClient _subbrandApi;
        private readonly IClientesAsignadosArolPorUsuariosApiClient _clientesAsignadosArolPorUsuariosApiClient;
        private readonly ICustomersApiClient _customersApi;
        private readonly IUserByRoleApiClient _userByRoleApiClient;
        private readonly ICustomersRolesUsersApiClient _customersRolesUsersApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IBusinessStructureApiClient _businessStructureApiClient;

        public UsuarioController(IUserApiClient apiClient, IBrandApiClient brandApi, IMapper mapper, IBusinessUnitApiClient businessUnitApi, 
                                 ISubbrandApiClient subbrandApi, IClientesAsignadosArolPorUsuariosApiClient clientesAsignadosArolPorUsuariosApiClient,
                                 ICustomersApiClient customersApi, IUserByRoleApiClient userByRoleApiClient, ICustomersRolesUsersApiClient customersRolesUsersApiClient,
                                 IRoleApiClient roleApiClient, IBusinessStructureApiClient businessStructureApiClient)
        {
            _apiClient = apiClient;
            _brandApi = brandApi;
            _mapper = mapper;
            _businessUnitApi = businessUnitApi;
            _subbrandApi = subbrandApi;
            _clientesAsignadosArolPorUsuariosApiClient = clientesAsignadosArolPorUsuariosApiClient;
            _customersApi = customersApi;
            _userByRoleApiClient = userByRoleApiClient;
            _customersRolesUsersApiClient = customersRolesUsersApiClient;
            _roleApiClient = roleApiClient;
            _businessStructureApiClient = businessStructureApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetAllUsersAsync();

            var dtos = _mapper.Map<List<UserDto>>(apiData);
            var usuarios = _mapper.Map<List<UsuarioViewModel>>(dtos);

            return View(usuarios);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetAllUsersAsync();

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
        public async Task<JsonResult> ListarZonas(List<int>? brandIds, List<int>? subbrandIds)
        {
            var apiData = await _businessStructureApiClient.GetBusinessStructuresByFiltersAsync(brandIds, subbrandIds, null);
            var zonas = apiData
                .GroupBy(z => z.ZonaId)
                .Select(g => new ZonaViewModel { Id = g.Key, Nombre = g.First().Zona ?? string.Empty })
                .ToList();

            return Json(new { success = true, data = zonas });
        }

        [HttpGet]
        public async Task<JsonResult> ListarClientesPorFiltros(List<int> brandIds , List<int> subbrandIds, List<int> zoneIds)
        {
            var apiData = await _customersApi.GetCustomersByFiltersAsync(brandIds, subbrandIds, zoneIds);
            var dtos = _mapper.Map<List<CustomerDto>>(apiData);
            var clientes = _mapper.Map<List<ClienteEstructuraViewModel>>(dtos);
            clientes.ForEach(c => c.Nombre = $"{c.ClienteId} - {c.Nombre} ");
            clientes = clientes.OrderBy(c => c.ClienteId).ToList();
            return Json(new { success = true, data = clientes });
        }

        [HttpPost]
        public async Task<JsonResult> GuardarRolPorUsuario([FromBody] UsuarioRolViewModel model)
        {
            try
            {
                if (model.Id > 0)
                {
                    try
                    {
                        var customers = await _customersRolesUsersApiClient.GetsByUserRolAsync(model.Id);
                        var existentes = customers.Select(c => c.ClienteId).ToList();
                        var seleccionados = model.ClienteIds ?? new List<long>();

                        var nuevos = seleccionados.Except(existentes).ToList();
                        var remover = customers.Where(c => !seleccionados.Contains(c.ClienteId)).Select(c => c.Id).ToList(); ;

                        if (nuevos.Any())
                        {
                            var addRequest = new CustomerRolUserRequest
                            {
                                RolPorUsuarioId = model.Id,
                                Clientes = nuevos,
                                GeolocalizacionActiva = true
                            };
                            await _customersRolesUsersApiClient.CreateAsync(addRequest);
                        }

                        if (remover.Count > 0) 
                        {
                            await _customersRolesUsersApiClient.RemoveByCustomerAsync(remover, 0);
                        }
                        
                        

                        if (!seleccionados.Any())
                        {
                            await _userByRoleApiClient.DeleteAsync(model.Id);
                        }

                        return Json(new { success = true, id = model.Id });
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, error = "Error al actualizar el rol del usuario: " + ex.Message });
                    }
                }

                int userRoleId = 0;

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

                string result = await _customersRolesUsersApiClient.CreateAsync(customerRolUserRequest);


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
                request.Estatus = model.Estatus;

                if (model.Id == 0)
                {
                    var id = await _apiClient.CreateAsync(request);
                    return Json(new { success = true, id });
                }
                else
                {
                    var updateRequest = _mapper.Map<UpdateUserRequest>(model);
                    updateRequest.Estatus = model.Estatus;
                    var updated = await _apiClient.UpdateAsync(updateRequest);
                    if (updated)
                        return Json(new { success = true, id = model.Id });

                    return Json(new { success = false, error = "No se pudo actualizar" });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = "Ocurrió un error inesperado. Favor de verificar si el usuario o el correo ya fueron registrados."
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EliminarUsuario(int id)
        {
            try
            {
                await _apiClient.DeleteAsync(id);
                return Json(new { success = true, message = "Usuario deshabilitado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al deshabilitar el usuario: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EliminarRolClienteDirecto(int id)
        {
            try
            {
                var customers = await _customersRolesUsersApiClient.GetsByUserRolAsync(id);
                var remover = customers.Select(c => c.Id).ToList();

                if (remover.Count > 0)
                {
                    await _customersRolesUsersApiClient.RemoveByCustomerAsync(remover, 0);
                }

                await _userByRoleApiClient.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Error al eliminar el rol: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerClientesPorRolPorUsuario(int id)
        {
            var userByRole = await _userByRoleApiClient.GetUserByRoleAsync(id);
            if (userByRole == null)
                return Json(new { success = false, error = "No encontrado" });

            var role = await _roleApiClient.GetRoleAsync((byte)userByRole.RolId);
            var customers = await _customersRolesUsersApiClient.GetsByUserRolAsync(id);

            var marcaIds = new List<int>();
            var submarcaIds = new List<int>();
            var zonaIds = new List<int>();
            foreach (var c in customers)
            {
                var structure = await _businessStructureApiClient.GetBusinessStructureByCustomerAsync((long)c.ClienteId);
                if (structure.FirstOrDefault()?.MarcaId != null)
                {
                    marcaIds.Add(structure.FirstOrDefault().MarcaId.Value);
                }

                if (structure.FirstOrDefault()?.SubmarcaId != null)
                {
                    submarcaIds.Add(structure.FirstOrDefault().SubmarcaId.Value);
                }

                if (structure.FirstOrDefault()?.ZonaId != null)
                {
                    zonaIds.Add(structure.FirstOrDefault().ZonaId);
                }
            }
            marcaIds = marcaIds.Distinct().ToList();
            submarcaIds = submarcaIds.Distinct().ToList();
            zonaIds = zonaIds.Distinct().ToList();

            var data = new
            {
                RolId = userByRole.RolId,
                UnidadDeNegocioId = role?.UnidadDeNegocioId,
                ClienteIds = customers.Select(c => c.ClienteId).ToList(),
                MarcaIds = marcaIds,
                SubmarcaIds = submarcaIds,
                ZonaIds = zonaIds
            };

            return Json(new { success = true, data });
        }

        [HttpGet]
        public async Task<JsonResult> ListarRolesPorUsuario(int usuarioId)
        {
            var apiData = await _userByRoleApiClient.GetByUserAsync(usuarioId);
            apiData = apiData
            .Where(x => x.Estatus)
            .ToList();
            var dtos = _mapper.Map<List<RelUserByRoleDto>>(apiData);
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

        [HttpGet]
        public async Task<IActionResult> DescargarReporte()
        {
            var base64 = await _apiClient.GetReport();
            var bytes = Convert.FromBase64String(base64);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteUsuarios.xlsx");
        }

        
    }
}
