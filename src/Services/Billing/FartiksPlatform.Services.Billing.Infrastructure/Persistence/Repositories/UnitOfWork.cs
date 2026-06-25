using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Infrastructure.Persistence.Configurations;

namespace FartiksPlatform.Services.Billing.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BillingDbContext _context;

    public UnitOfWork(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
