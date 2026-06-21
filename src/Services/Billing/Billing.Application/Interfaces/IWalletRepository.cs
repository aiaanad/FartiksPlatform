using Billing.Domain.Entities;
using Billing.Domain.Enums;

namespace Billing.Application.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByPlayerAndCurrencyAsync(Guid playerId, CurrencyType currency);
    Task AddAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
}
