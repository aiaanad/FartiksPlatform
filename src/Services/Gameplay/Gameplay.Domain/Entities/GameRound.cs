using Gameplay.Domain.Enums;

namespace Gameplay.Domain.Entities;

public class GameRound
{
    public Guid Id { get; init; }
    public Guid PlayerId { get; init; }
    public Guid GameId { get; init; }
    public decimal BetAmount { get; init; }
    public string OutcomeJson { get; init; } = string.Empty;
    public GameResult Result { get; init; }
    public decimal PayoutAmount { get; init; }
    public DateTime PlayedAt { get; init; }
}
