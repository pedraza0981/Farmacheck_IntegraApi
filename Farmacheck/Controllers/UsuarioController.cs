using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Users;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Farmacheck.Application.DTOs;

namespace Farmacheck.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserApiClient _apiClient;
        private readonly IBrandApiClient _brandApi;
        private readonly IMapper _mapper;

        public UsuarioController(IUserApiClient apiClient, IBrandApiClient brandApi, IMapper mapper)
        {
            _apiClient = apiClient;
            _brandApi = brandApi;
            _mapper = mapper;
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
        public async Task<JsonResult> ListarMarcas(int unidadId)
        {
            var apiData = await _brandApi.GetBrandsByBusinessUnitAsync(unidadId);
            var dtos = _mapper.Map<List<MarcaDto>>(apiData);
            var marcas = _mapper.Map<List<MarcaViewModel>>(dtos);

            return Json(new { success = true, data = marcas });
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
    }
}
