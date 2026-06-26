using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Domain.Constants;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;

namespace FartiksPlatform.Services.Gameplay.Domain.Strategies;

public class BlackjackStrategy : IOutcomeStrategy
{
    private readonly IRandomProvider _randomProvider;

    public BlackjackStrategy(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public bool CanHandle(string gameType) => gameType == GameType.Blackjack;

    public string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount)
    {
        int playerPoints = _randomProvider.Next(15, 26);
        int dealerPoints = _randomProvider.Next(15, 26);

        if (playerPoints > 21)
        {
            payoutAmount = 0;
            return GameResult.Loss;
        }
        if (dealerPoints > 21 || playerPoints > dealerPoints)
        {
            payoutAmount = betAmount * 2;
            return GameResult.Win;
        }
        if (playerPoints == dealerPoints)
        {
            payoutAmount = betAmount;
            return GameResult.Draw;
        }

        payoutAmount = 0;
        return GameResult.Loss;
    }
}
