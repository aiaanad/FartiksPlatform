using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(AppUser user);
    string RefreshToken(AppUser user);
}
