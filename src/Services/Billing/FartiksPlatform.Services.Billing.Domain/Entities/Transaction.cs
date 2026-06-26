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
    public SourceType SourceType { get; init; }
    public TransactionStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }

    public static Transaction Create(
        Guid playerId,
        TransactionType type,
        decimal amount,
        decimal balanceBefore,
        decimal balanceAfter,
        Guid? sourceId,
        SourceType sourceType)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            Type = type,
            Amount = amount,
            BalanceBefore = balanceBefore,
            BalanceAfter = balanceAfter,
            SourceId = sourceId,
            SourceType = sourceType,
            Status = TransactionStatus.Completed,
            CreatedAt = DateTime.UtcNow
        };
    }
}
