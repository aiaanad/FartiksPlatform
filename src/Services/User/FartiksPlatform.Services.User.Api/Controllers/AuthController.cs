using System.Security.Claims;
using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Api.Contracts;
using FartiksPlatform.Services.User.Application.Commands.LoginUser;
using FartiksPlatform.Services.User.Application.Commands.RegisterUser;
using FartiksPlatform.Services.User.Application.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FartiksPlatform.Services.User.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        Result result = await _mediator.Send(
            new RegisterUserCommand(request.Username, request.Email, request.Password, request.Role));
        return result.IsSuccess ? Ok() : result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        Result<Application.Commands.LoginUser.LoginUserResponse> result = await _mediator.Send(new LoginUserCommand(request.Email, request.Password));
        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost("verify-email")]
    [Authorize]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("sub")?.Value;
        if (!Guid.TryParse(userIdClaim, out Guid userId))
            return Unauthorized();

        Result result = await _mediator.Send(new VerifyEmailCommand(userId, request.VerificationCode));
        return result.IsSuccess ? Ok() : result.ToActionResult();
    }

    [HttpPost("refresh-token")]
    public Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        // Команды/хендлера для refresh-token пока нет — пока это пустая заглушка.
        // проекте нет CQRS-команды и хендлера, которые бы обновляли токен  
        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status501NotImplemented));
    }
}

public record RefreshTokenRequest(string RefreshToken);
