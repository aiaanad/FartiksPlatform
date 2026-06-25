namespace FartiksPlatform.Services.User.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string tokenHash);
    Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId);
    Task AddAsync(RefreshToken refreshToken);
    void Update(RefreshToken refreshToken);
    void Delete(RefreshToken refreshToken);
    Task RevokeAllUserTokensAsync(Guid userId, string reason);
}