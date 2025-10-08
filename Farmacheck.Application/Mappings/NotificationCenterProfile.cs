using AutoMapper;
using Farmacheck.Application.DTOs;
using Farmacheck.Application.Models.NotificationCenter;

namespace Farmacheck.Application.Mappings;

public class NotificationCenterProfile : Profile
{
    public NotificationCenterProfile()
    {
        CreateMap<NotificationTypeResponse, NotificationTypeDto>();

        CreateMap<NotificationResponse, NotificationDto>();

        CreateMap<NotificationFormatResponse, NotificationFormatDto>();

        CreateMap<NotificationSettingResponse, NotificationSettingDto>();
    }
}
