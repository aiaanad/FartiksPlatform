using FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;
using FartiksPlatform.Services.Gameplay.Application.UseCases.PlayGame;
using FartiksPlatform.Services.Gameplay.Application.UseCases.GetRoundHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.Gameplay.Api.Controllers;

[ApiController]
[Route("api/games")]
public class GamesController : ControllerBase
{
    private readonly ISender _sender;

    public GamesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] string? type, CancellationToken cancellationToken)
    {
        var query = new GetGamesQuery(type);
        Result<GetGamesResponse> result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("play")]
    public async Task<IActionResult> Play([FromBody] PlayGameCommand command, CancellationToken cancellationToken)
    {
        Result<PlayGameResponse> result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("rounds/history")]
    public async Task<IActionResult> RoundsHistory([FromQuery] Guid playerId, CancellationToken cancellationToken)
    {
        var query = new GetRoundHistoryQuery(playerId);
        Result<GetRoundHistoryResponse> result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
