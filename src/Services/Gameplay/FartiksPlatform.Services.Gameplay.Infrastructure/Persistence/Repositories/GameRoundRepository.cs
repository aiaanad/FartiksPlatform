using Microsoft.EntityFrameworkCore;
using FartiksPlatform.Services.Gameplay.Domain.Repositories;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Infrastructure.Persistence;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Repositories;

public class GameRoundRepository : IGameRoundRepository
{
    private readonly GameplayDbContext _context;
    private readonly DbSet<GameRound> _gameRounds;

    public GameRoundRepository(GameplayDbContext context)
    {
        _context = context;
        _gameRounds = context.GameRounds;
    }

    public async Task<GameRound?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Include(gr => gr.GameId)
            .FirstOrDefaultAsync(gr => gr.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<GameRound>> GetByPlayerIdAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .OrderByDescending(gr => gr.PlayedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GameRound>> GetByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.GameId == gameId)
            .OrderByDescending(gr => gr.PlayedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GameRound>> GetByPlayerAndGameAsync(
        Guid playerId,
        Guid gameId,
        CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId && gr.GameId == gameId)
            .OrderByDescending(gr => gr.PlayedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GameRound>> GetRecentByPlayerAsync(
        Guid playerId,
        int count,
        CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .OrderByDescending(gr => gr.PlayedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GameRound gameRound, CancellationToken cancellationToken = default)
    {
        await _gameRounds.AddAsync(gameRound, cancellationToken);
    }

    public void Update(GameRound gameRound)
    {
        _gameRounds.Update(gameRound);
    }

    public void Delete(GameRound gameRound)
    {
        _gameRounds.Remove(gameRound);
    }

    public async Task<GameRound?> GetLastByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .OrderByDescending(gr => gr.PlayedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalBetByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .SumAsync(gr => gr.BetAmount, cancellationToken);
    }

    public async Task<decimal> GetTotalPayoutByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .SumAsync(gr => gr.PayoutAmount, cancellationToken);
    }

    public async Task<int> GetCountByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default)
    {
        return await _gameRounds
            .AsNoTracking()
            .Where(gr => gr.PlayerId == playerId)
            .CountAsync(cancellationToken);
    }
}
