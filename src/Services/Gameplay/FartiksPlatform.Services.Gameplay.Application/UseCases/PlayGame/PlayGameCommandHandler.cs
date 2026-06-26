using System.Text.Json;
using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.BuildingBlocks.Errors;
using FartiksPlatform.Services.Gameplay.Application.Abstractions;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using FartiksPlatform.Services.Gameplay.Domain.Strategies;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;
using MediatR;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.PlayGame;

public class PlayGameCommandHandler : IRequestHandler<PlayGameCommand, Result<PlayGameResponse>>
{
    private readonly IGameplayUnitOfWork _unitOfWork;
    private readonly IBillingClient _billingClient;
    private readonly IEnumerable<IOutcomeStrategy> _strategies;

    public PlayGameCommandHandler(
        IGameplayUnitOfWork unitOfWork,
        IBillingClient billingClient,
        IEnumerable<IOutcomeStrategy> strategies)
    {
        _unitOfWork = unitOfWork;
        _billingClient = billingClient;
        _strategies = strategies;
    }

    public async Task<Result<PlayGameResponse>> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        Game? game = await _unitOfWork.Games.GetByIdAsync(request.GameId, cancellationToken);
        if (game == null)
        {
            return Result.Failure<PlayGameResponse>(new Error("Game.NotFound", "Игра не найдена."));
        }

        var isWithdrawn = await _billingClient.CheckAndWithdrawBetAsync(request.PlayerId, request.BetAmount, "USD");
        if (!isWithdrawn)
        {
            return Result.Failure<PlayGameResponse>(new Error("Billing.InsufficientFunds", "Недостаточно средств или ошибка списания."));
        }

        IOutcomeStrategy? strategy = _strategies.FirstOrDefault(s =>
        {
            return s.CanHandle(game.Type);
        });
        if (strategy == null)
        {
            return Result.Failure<PlayGameResponse>(new Error("Strategy.NotFound", $"Стратегия для {game.Type} не найдена."));
        }

        var roundId = Guid.NewGuid();

        string gameResult = strategy.CalculateOutcome(game, request.BetAmount, out decimal payoutAmount);

        if (payoutAmount > 0)
        {
            await _billingClient.CreditWinAsync(request.PlayerId, payoutAmount, "USD", roundId);
        }

        var gameRound = new GameRound
        {
            Id = roundId,
            PlayerId = request.PlayerId,
            GameId = game.Id,
            BetAmount = request.BetAmount,
            Result = gameResult,
            PayoutAmount = payoutAmount,
            OutcomeJson = JsonSerializer.Serialize(new { Result = gameResult }),
            PlayedAt = DateTime.UtcNow
        };

        await _unitOfWork.Rounds.AddAsync(gameRound, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new PlayGameResponse(
            gameRound.Id,
            gameRound.Result,
            gameRound.PayoutAmount,
            gameRound.OutcomeJson
        );

        return Result.Success(response);
    }
}
