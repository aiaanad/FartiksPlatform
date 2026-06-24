using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Domain.Entities;
using FartiksPlatform.Services.Billing.Domain.Enums;
using FartiksPlatform.Services.Billing.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FartiksPlatform.Services.Billing.Infrastructure.Persistence.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly BillingDbContext _context;
    
    public WalletRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet?> GetByPlayerAndCurrencyAsync(Guid playerId, CurrencyType currency)
    {
        return await  _context.Wallets.FirstOrDefaultAsync(w => w.PlayerId == playerId && w.Currency == currency);
    }

    public async Task AddAsync(Wallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        await Task.CompletedTask;
    }
}
