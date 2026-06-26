namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetRoundHistory;

public record RoundHistoryDto(
    Guid RoundId,
    Guid GameId,
    decimal BetAmount,
    string Result,
    decimal PayoutAmount,
    string OutcomeJson,
    DateTime PlayedAt
);
