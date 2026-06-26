namespace FartiksPlatform.Services.Gameplay.Application.Abstractions;

public interface IBillingClient
{
    Task<bool> CheckAndWithdrawBetAsync(Guid playerId, decimal amount, string currency);
    Task CreditWinAsync(Guid playerId, decimal amount, string currency, Guid roundId);
}
