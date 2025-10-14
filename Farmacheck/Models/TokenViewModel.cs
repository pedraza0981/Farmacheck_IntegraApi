namespace Farmacheck.Models;

public class TokenViewModel
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public int? UserId { get; set; }
}
