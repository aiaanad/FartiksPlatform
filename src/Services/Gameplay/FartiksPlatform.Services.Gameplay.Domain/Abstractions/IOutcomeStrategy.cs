using FartiksPlatform.Services.Gameplay.Domain.Entities;

namespace FartiksPlatform.Services.Gameplay.Domain.Strategies;

public interface IOutcomeStrategy
{
    bool CanHandle(string gameType);
    string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount);
}
