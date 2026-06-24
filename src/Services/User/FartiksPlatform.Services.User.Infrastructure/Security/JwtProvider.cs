using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(JwtOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public string GenerateToken(AppUser user)
    {
        throw new NotImplementedException();
    }

    public string RefreshToken(AppUser user)
    {
        throw new NotImplementedException();
    }
}
