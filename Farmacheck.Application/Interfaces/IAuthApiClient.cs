using Farmacheck.Application.Models.Security;

namespace Farmacheck.Application.Interfaces;

public interface IAuthApiClient
{
    Task<bool> ValidateAsync(string token);
    Task<UserInfoResponse?> GetUserInfoAsync(string token);
    Task<TokenResponse?> LoginAsync(CredentialsRequest request);
    Task<TokenResponse?> RefreshAsync(RegenerateRequest request);
    Task<bool> LogoutAsync(string token);
}
