using FartiksPlatform.Services.Billing.Domain.Enums;

namespace FartiksPlatform.Services.Billing.Domain.Entities;

public class Transaction
{
    public Guid Id { get; init; }
    public Guid PlayerId { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    public decimal BalanceBefore { get; init; }
    public decimal BalanceAfter { get; init; }
    public Guid? SourceId { get; init; }
    public int SourceType { get; init; } // GAME_ROUND/ADMIN_ACTION
    public TransactionStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
}
