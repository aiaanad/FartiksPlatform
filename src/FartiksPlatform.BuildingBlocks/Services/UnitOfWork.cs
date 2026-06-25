namespace FartiksPlatform.BuildingBlocks.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext _context;
    private IDbContextTransaction _transaction;
    
    public UnitOfWork(UserDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        await _transaction?.CommitAsync();
    }
    
    public async Task RollbackTransactionAsync()
    {
        await _transaction?.RollbackAsync();
    }
}