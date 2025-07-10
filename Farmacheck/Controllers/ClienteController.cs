using Microsoft.AspNetCore.Mvc;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Farmacheck.Infrastructure.Interfaces;
using Farmacheck.Infrastructure.Models.Customers;
using Farmacheck.Application.DTOs;

namespace Farmacheck.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ICustomersApiClient _apiClient;
        private readonly ICustomerTypesApiClient _customerTypesApi;
        private readonly IZoneApiClient _zoneApi;
        private readonly IBusinessStructureApiClient _businessStructureApi;
        private readonly IMapper _mapper;
        private readonly IBrandApiClient _ibrand;

        public ClienteController(
            ICustomersApiClient apiClient,
            ICustomerTypesApiClient customerTypesApi,
            IZoneApiClient zoneApi,
            IBusinessStructureApiClient businessStructureApi,
            IMapper mapper,
            IBrandApiClient ibrand)
        {
            _apiClient = apiClient;
            _customerTypesApi = customerTypesApi;
            _zoneApi = zoneApi;
            _businessStructureApi = businessStructureApi;
            _mapper = mapper;
            _ibrand = ibrand;
        }

        public async Task<IActionResult> Index()
        {
            var apiData = await _apiClient.GetCustomersAsync();
            var dtos = _mapper.Map<List<CustomerDto>>(apiData);
            var clientes = _mapper.Map<List<ClienteEstructuraViewModel>>(dtos);
            return View(clientes);
        }

        [HttpGet]
        public async Task<JsonResult> Listar()
        {
            var apiData = await _apiClient.GetCustomersAsync();
            var dtos = _mapper.Map<List<CustomerDto>>(apiData);
            var clientes = _mapper.Map<List<ClienteEstructuraViewModel>>(dtos);
            return Json(new { success = true, data = clientes });
        }

        [HttpGet]
        public async Task<JsonResult> Obtener(int id)
        {
            try
            {
                var entidad = await _apiClient.GetCustomerAsync(id);
                if (entidad == null)
                    return Json(new { success = false, error = "No encontrado" });

                var dto = _mapper.Map<CustomerDto>(entidad);

                var businessStructure = await _businessStructureApi.GetBusinessStructureByCustomerAsync(id);

                var marca = await _ibrand.GetBrandAsync(businessStructure.MarcaId);

                if (businessStructure != null)
                {
                    var bsDto = _mapper.Map<BusinessStructureDto>(businessStructure);

                    if(marca!= null)
                        bsDto.UnidadDeNegocioId = marca.UnidadDeNegocio;

                    dto.BusinessStructure = bsDto;
                }

                var model = _mapper.Map<ClienteEstructuraViewModel>(dto);
                return Json(new { success = true, data = model });
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Error al obtener cliente con ID {id}", id);
                return Json(new
                {
                    success = false,
                    error = "Ha ocurrido un error inesperado.",
                    detail = ex.Message // opcional en producción
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] ClienteEstructuraViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nombre))
                return Json(new { success = false, error = "El nombre es obligatorio." });

            if (model.ClienteId == 0)
            {
                var request = _mapper.Map<CustomerRequest>(model);
                var id = await _apiClient.CreateAsync(request);

                var bsRequest = _mapper.Map<BusinessStructureRequest>(model);
                bsRequest.ClienteId = id;
                await _businessStructureApi.CreateAsync(bsRequest);

                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateCustomerRequest>(model);
                var updated = await _apiClient.UpdateAsync(updateRequest);
                if (updated)
                {
                    var bsUpdate = _mapper.Map<UpdateBusinessStructureRequest>(model);
                    bsUpdate.ClienteId = model.ClienteId;
                    await _businessStructureApi.UpdateAsync(bsUpdate);

                    return Json(new { success = true, id = model.ClienteId });
                }

                return Json(new { success = false, error = "No se pudo actualizar" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            await _businessStructureApi.DeleteAsync(id);
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<JsonResult> ListarTiposCliente()
        {
            var apiData = await _customerTypesApi.GetCustomerTypesAsync();
            var dtos = _mapper.Map<List<CustomerTypeDto>>(apiData);
            var items = _mapper.Map<List<SelectListItem>>(dtos);
            return Json(new { success = true, data = items });
        }

        [HttpGet]
        public async Task<JsonResult> ListarZonas()
        {
            var apiData = await _zoneApi.GetZonesAsync();
            var dtos = _mapper.Map<List<ZoneDto>>(apiData);
            var items = _mapper.Map<List<SelectListItem>>(dtos);
            return Json(new { success = true, data = items });
        }
    }
}
