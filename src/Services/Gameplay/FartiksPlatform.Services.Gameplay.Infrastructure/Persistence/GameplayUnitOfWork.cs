using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using FartiksPlatform.Services.Gameplay.Domain.Repositories;
using FartiksPlatform.Services.Gameplay.Application.Abstractions;
using FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Repositories;

namespace FartiksPlatform.Services.Gameplay.Infrastructure.Persistence;

public class GameplayUnitOfWork : IGameplayUnitOfWork
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;

    private readonly GameplayDbContext _context;
    private IDbContextTransaction? _transaction;

    public IGameRepository Games => _gameRepository;
    public IGameRoundRepository Rounds => _gameRoundRepository;

    public GameplayUnitOfWork(GameplayDbContext context, IGameRepository gameRepository, IGameRoundRepository gameRoundRepository)
    {
        _context = context;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
