namespace FartiksPlatform.Services.User.Infrastructure.Security;

public class JwtOptions
{
    public string Secret { get; set; } = string.Empty;
    public int ExpirationMinutes { get; set; }
    public int RefreshExpirationMinutes { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}
