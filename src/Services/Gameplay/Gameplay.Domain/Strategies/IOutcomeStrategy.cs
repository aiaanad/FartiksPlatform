using Gameplay.Domain.Entities;

namespace Gameplay.Domain.Strategies;

public interface IOutcomeStrategy
{
    string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount);
}
