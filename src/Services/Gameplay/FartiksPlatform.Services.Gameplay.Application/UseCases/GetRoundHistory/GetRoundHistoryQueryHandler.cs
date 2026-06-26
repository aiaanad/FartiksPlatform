using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.Gameplay.Application.Abstractions;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using MediatR;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetRoundHistory;

public class GetRoundHistoryQueryHandler : IRequestHandler<GetRoundHistoryQuery, Result<GetRoundHistoryResponse>>
{
    private readonly IGameplayUnitOfWork _unitOfWork;

    public GetRoundHistoryQueryHandler(IGameplayUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetRoundHistoryResponse>> Handle(GetRoundHistoryQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<GameRound> rounds = await _unitOfWork.Rounds.GetByPlayerIdAsync(request.PlayerId, cancellationToken);

        var historyDtos = rounds.Select(r =>
        {
            return new RoundHistoryDto(
                        r.Id,
                        r.GameId,
                        r.BetAmount,
                        r.Result,
                        r.PayoutAmount,
                        r.OutcomeJson,
                        r.PlayedAt
                    );
        }).ToList();

        var response = new GetRoundHistoryResponse(historyDtos);

        return Result.Success(response);
    }
}
