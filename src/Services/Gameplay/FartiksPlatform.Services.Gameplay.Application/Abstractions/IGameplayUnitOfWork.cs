using FartiksPlatform.Services.Gameplay.Domain.Repositories;
namespace FartiksPlatform.Services.Gameplay.Application.Abstractions;

public interface IGameplayUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    void Dispose();
    IGameRepository Games { get; }
    IGameRoundRepository Rounds { get; }
}
