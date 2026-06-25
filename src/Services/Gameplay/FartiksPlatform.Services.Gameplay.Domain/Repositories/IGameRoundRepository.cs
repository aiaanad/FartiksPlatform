using FartiksPlatform.Services.Gameplay.Domain.Entities;

namespace FartiksPlatform.Services.Gameplay.Domain.Repositories;

public interface IGameRoundRepository
{
    Task<GameRound?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<GameRound>> GetByPlayerIdAsync(Guid playerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<GameRound>> GetByGameIdAsync(Guid gameId, CancellationToken cancellationToken = default);
    Task<IEnumerable<GameRound>> GetByPlayerAndGameAsync(Guid playerId, Guid gameId, CancellationToken cancellationToken = default);
    Task<IEnumerable<GameRound>> GetRecentByPlayerAsync(Guid playerId, int count, CancellationToken cancellationToken = default);
    Task AddAsync(GameRound gameRound, CancellationToken cancellationToken = default);
    void Update(GameRound gameRound);
    void Delete(GameRound gameRound);
    Task<GameRound?> GetLastByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalBetByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalPayoutByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default);
    Task<int> GetCountByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default);
}
