using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.NotificationCenter;
using Farmacheck.Models;
using Microsoft.AspNetCore.Mvc;

namespace Farmacheck.Controllers
{
    public class CentroNotificacionController : Controller
    {
        private readonly INotificationCenterApiClient _apiClient;
        private readonly IMapper _mapper;
        public CentroNotificacionController(INotificationCenterApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var items = await GetNotificationSetting(); ;
            return View(items);
        }

        [HttpPost]
        public async Task<JsonResult> Guardar([FromBody] NotificationSettingPostViewModel model)
        {
            try
            {

                var request = _mapper.Map<NotificationSettingRequest>(model);
                var result = await _apiClient.CreateAsync(request);

                if(!result)
                    return Json(new { success = false, error = "No se pudo guardar la configuración" });
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Ocurrió un error inesperado: " + ex.Message });
            }
        }

        public async Task<List<NotificationTypeViewModel>> GetNotificationSetting()
        {
            var settings = await _apiClient.GetNotificationCenter();
            var currentSettings = await _apiClient.GetNotificationSetting();

            // Si no hay configuraciones, solo proyectamos settings
            if (settings.Any() && !currentSettings.Any())
            {
                var dtos = _mapper.Map<List<NotificationTypeDto>>(settings);
                return _mapper.Map<List<NotificationTypeViewModel>>(dtos);
            }
            
            var notificacionIds = currentSettings.Select(c => c.IdNotificacion).ToHashSet();
            var combinaciones = currentSettings
                .Select(c => (c.IdNotificacion, c.IdFormatoNotificacion))
                .ToHashSet();

            var result = settings.Select(x =>
                new NotificationTypeViewModel
                {
                    Id = x.Id,
                    TipoNotificacion = x.TipoNotificacion,
                    Notificaciones = x.Notificaciones!.Select(n => new NotificationViewModel
                    {
                        Id = n.Id,
                        Descripcion = n.Descripcion,
                        IsChecked = notificacionIds.Contains(n.Id),
                        FormatosNotificaciones = n.FormatosNotificaciones!
                            .Select(f => new NotificationFormatViewModel
                            {
                                Id = f.Id,
                                Descripcion = f.Descripcion,
                                IsChecked = combinaciones.Contains((n.Id, f.Id))
                            }).ToList()
                    }).ToList()
                }).ToList();

            return result;
        }

    }
}
