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
        private readonly IMapper _mapper;

        private static readonly List<SelectListItem> _tiposCliente = new()
        {
            new SelectListItem { Value = "1", Text = "Tipo A" },
            new SelectListItem { Value = "2", Text = "Tipo B" }
        };

        private static readonly List<SelectListItem> _zonas = new()
        {
            new SelectListItem { Value = "1", Text = "Zona Norte" },
            new SelectListItem { Value = "2", Text = "Zona Sur" }
        };
        public ClienteController(ICustomersApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
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
            var entidad = await _apiClient.GetCustomerAsync(id);
            if (entidad == null)
                return Json(new { success = false, error = "No encontrado" });

            var dto = _mapper.Map<CustomerDto>(entidad);
            var model = _mapper.Map<ClienteEstructuraViewModel>(dto);
            return Json(new { success = true, data = model });
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
                return Json(new { success = true, id });
            }
            else
            {
                var updateRequest = _mapper.Map<UpdateCustomerRequest>(model);
                var updated = await _apiClient.UpdateAsync(updateRequest);
                if (updated)
                    return Json(new { success = true, id = model.ClienteId });

                return Json(new { success = false, error = "No se pudo actualizar" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Eliminar(int id)
        {
            await _apiClient.DeleteAsync(id);
            return Json(new { success = true });
        }


        [HttpGet]
        public JsonResult ListarTiposCliente()
        {
            return Json(new { success = true, data = _tiposCliente });
        }

        [HttpGet]
        public JsonResult ListarZonas()
        {
            return Json(new { success = true, data = _zonas });
        }
    }
}
