using FartiksPlatform.Services.Billing.Domain.Entities;
using FartiksPlatform.Services.Billing.Domain.Enums;

namespace FartiksPlatform.Services.Billing.Application.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByPlayerAndCurrencyAsync(Guid playerId, CurrencyType currency);
    Task AddAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
}
