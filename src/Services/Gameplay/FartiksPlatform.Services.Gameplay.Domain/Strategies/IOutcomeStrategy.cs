using FartiksPlatform.Services.Gameplay.Domain.Entities;

namespace FartiksPlatform.Services.Gameplay.Domain.Strategies;

public interface IOutcomeStrategy
{
    string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount);
}
