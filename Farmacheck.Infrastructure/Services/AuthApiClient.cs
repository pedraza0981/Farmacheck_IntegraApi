using System.Net.Http.Headers;
using System.Net.Http.Json;
using Farmacheck.Application.Interfaces;
using Farmacheck.Application.Models.Security;

namespace Farmacheck.Infrastructure.Services;

public class AuthApiClient : IAuthApiClient
{
    private readonly HttpClient _http;

    public AuthApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> ValidateAsync(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/security/Auth");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
        return result?.Data ?? false;
    }

    public async Task<UserInfoResponse?> GetUserInfoAsync(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/security/Auth/user");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserInfoResponse>>();
        return result?.Data;
    }

    public async Task<TokenResponse?> LoginAsync(CredentialsRequest requestModel)
    {
        var response = await _http.PostAsJsonAsync("api/v1/security/Auth", requestModel);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }

    public async Task<TokenResponse?> RefreshAsync(RegenerateRequest requestModel)
    {
        var response = await _http.PutAsJsonAsync("api/v1/security/Auth", requestModel);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }

    public async Task<bool> LogoutAsync(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, "api/v1/security/Auth");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<bool>();
        return result;
    }

    private class ApiResponse<T>
    {
        public T? Data { get; set; }
    }
}
