using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Domain.Constants;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;

namespace FartiksPlatform.Services.Gameplay.Domain.Strategies;

public class RouletteStrategy : IOutcomeStrategy
{
    private readonly IRandomProvider _randomProvider;

    public RouletteStrategy(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public bool CanHandle(string gameType) => gameType == GameType.Roulette;

    public string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount)
    {
        int ballPosition = _randomProvider.Next(0, 37);

        if (ballPosition == 0)
        {
            payoutAmount = 0;
            return GameResult.Loss;
        }

        if (ballPosition % 2 == 0)
        {
            payoutAmount = betAmount * 2;
            return GameResult.Win;
        }

        payoutAmount = 0;
        return GameResult.Loss;
    }
}
