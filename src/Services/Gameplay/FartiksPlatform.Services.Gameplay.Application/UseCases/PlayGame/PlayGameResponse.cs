namespace FartiksPlatform.Services.Gameplay.Application.UseCases.PlayGame;

public record PlayGameResponse(
    Guid RoundId,
    string GameResult,
    decimal PayoutAmount,
    string OutcomeJson
);
