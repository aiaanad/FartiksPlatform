using FartiksPlatform.BuildingBlocks.Common;
using MediatR;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;

public record GetGamesQuery(string? GameType = null) : IRequest<Result<GetGamesResponse>>;
