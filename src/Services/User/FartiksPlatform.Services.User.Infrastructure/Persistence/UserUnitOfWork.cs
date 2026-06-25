using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FartiksPlatform.Services.User.Infrastructure.Persistence;

public class UserUnitOfWork : IUserUnitOfWork
{
    private readonly UserDbContext _context;
    private IUserRepository? _userRepository;
    private IRefreshTokenRepository? _refreshTokenRepository;
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;

    public UserUnitOfWork(UserDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users
        => _userRepository ??= new UserRepository(_context);

    public IRefreshTokenRepository RefreshTokens
        => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
            return;

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();

            if (_currentTransaction != null)
                await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _currentTransaction?.Dispose();
            _context.Dispose();
        }
        _disposed = true;
    }
}
