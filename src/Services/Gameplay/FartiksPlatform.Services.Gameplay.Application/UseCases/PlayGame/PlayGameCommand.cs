using FartiksPlatform.BuildingBlocks.Common;
using MediatR;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.PlayGame;

public record PlayGameCommand(
    Guid PlayerId,
    Guid GameId,
    decimal BetAmount
) : IRequest<Result<PlayGameResponse>>;
