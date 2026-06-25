using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Infrastructure.Persistence.Configurations;
using FartiksPlatform.Services.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FartiksPlatform.Services.Billing.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BillingDbContext _context;

    public TransactionRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<IEnumerable<Transaction>> GetByPlayerIdAsync(Guid playerId)
    {
        return await _context.Transactions
            .Where(t => t.PlayerId == playerId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
