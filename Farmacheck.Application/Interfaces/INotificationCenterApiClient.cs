using Farmacheck.Application.Models.NotificationCenter;

namespace Farmacheck.Application.Interfaces;

public interface INotificationCenterApiClient
{
    Task<IEnumerable<NotificationTypeResponse>> GetNotificationCenter();

    Task<IEnumerable<NotificationSettingResponse>> GetNotificationSetting();

    Task<bool> CreateAsync(NotificationSettingRequest request);
}
