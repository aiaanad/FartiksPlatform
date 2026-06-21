using FartiksPlatform.Services.Billing.Domain.Entities;

namespace FartiksPlatform.Services.Billing.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetByPlayerIdAsync(Guid playerId);
}
