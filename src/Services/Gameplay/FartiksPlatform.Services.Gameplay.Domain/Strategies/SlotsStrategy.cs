using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Domain.Constants;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;

namespace FartiksPlatform.Services.Gameplay.Domain.Strategies;

public class SlotsStrategy : IOutcomeStrategy
{
    private readonly IRandomProvider _randomProvider;
    private static readonly string[] Symbols = { "A", "B", "C", "D", "E" };

    public SlotsStrategy(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public bool CanHandle(string gameType) => gameType == GameType.Slots;

    public string CalculateOutcome(Game game, decimal betAmount, out decimal payoutAmount)
    {
        string s1 = Symbols[_randomProvider.Next(0, Symbols.Length)];
        string s2 = Symbols[_randomProvider.Next(0, Symbols.Length)];
        string s3 = Symbols[_randomProvider.Next(0, Symbols.Length)];

        if (s1 == s2 && s2 == s3)
        {
            payoutAmount = betAmount * 10;
            return GameResult.Win;
        }
        if (s1 == s2 || s2 == s3 || s1 == s3)
        {
            payoutAmount = betAmount * 1;
            return GameResult.Win;
        }

        payoutAmount = 0;
        return GameResult.Loss;
    }
}
