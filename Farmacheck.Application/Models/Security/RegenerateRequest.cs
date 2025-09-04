namespace Farmacheck.Application.Models.Security;

public class RegenerateRequest
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
