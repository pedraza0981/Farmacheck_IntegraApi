using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Brands;
using Farmacheck.Application.Models.CategoriesByQuestionnaires;
using Farmacheck.Application.Models.Common;
using Farmacheck.Application.Models.NotificationCenter;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Farmacheck.Infrastructure.Services
{
    public class NotificationCenterSettingApiClient : INotificationCenterApiClient
    {
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationCenterSettingApiClient(HttpClient http, IHttpContextAccessor httpContextAccessor)
        {
            _http = http;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddBearerToken()
        {
            if (_http.DefaultRequestHeaders.Authorization != null)
            {
                return;
            }

            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<NotificationTypeResponse>> GetNotificationCenter()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<NotificationTypeResponse>>("api/v1/NotificationCenter/settings")
                   ?? Enumerable.Empty<NotificationTypeResponse>();
        }

        public async Task<IEnumerable<NotificationSettingResponse>> GetNotificationSetting()
        {
            AddBearerToken();
            return await _http.GetFromJsonAsync<IEnumerable<NotificationSettingResponse>>("api/v1/NotificationCenter/notifications")
                   ?? Enumerable.Empty<NotificationSettingResponse>();
        }

        public async Task<bool> CreateAsync(NotificationSettingRequest request)
        {
            AddBearerToken();
            var response = await _http.PostAsJsonAsync("api/v1/NotificationCenter", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}

