using Microsoft.EntityFrameworkCore;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Entities;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Persistence;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly UserDbContext _context;

    public RefreshTokenRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string tokenHash)
    {
        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == tokenHash);
    }

    public async Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public void Update(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
    }

    public void Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
    }

    public async Task RevokeAllUserTokensAsync(Guid userId, string reason)
    {
        List<RefreshToken> tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.IsActive)
            .ToListAsync();

        foreach (RefreshToken? token in tokens)
        {
            token.Revoke(reason);
        }
    }
}
