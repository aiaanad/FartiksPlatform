using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.Gameplay.Application.Abstractions;
using FartiksPlatform.Services.Gameplay.Domain.Repositories;
using FartiksPlatform.Services.Gameplay.Domain.Entities;
using MediatR;

namespace FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, Result<GetGamesResponse>>
{
    private readonly IGameRepository _gameRepository;

    public GetGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Result<GetGamesResponse>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Game> games;

        if (!string.IsNullOrWhiteSpace(request.GameType))
        {
            games = await _gameRepository.GetByTypeAsync(request.GameType.ToUpper(), cancellationToken);
        }
        else
        {
            games = await _gameRepository.GetAllAsync(cancellationToken);
        }

        var gameDtos = games.Select(game =>
        {
            return new GameDto(
                        game.Id,
                        game.Name,
                        game.Type,
                        game.Description
                    );
        }).ToList();

        var response = new GetGamesResponse(gameDtos);

        return Result.Success(response);
    }
}
