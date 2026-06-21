using User.Domain.Entities;

namespace User.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(Player player);
}
