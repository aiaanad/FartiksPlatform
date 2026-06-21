using FartiksPlatform.Services.User.Api.Contracts;
using FartiksPlatform.Services.User.Application.Commands.LoginUser;
using FartiksPlatform.Services.User.Application.Commands.RegisterUser;
using FartiksPlatform.Services.User.Application.Commands.VerifyEmail;
using MediatR;
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
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        throw new NotImplementedException();
    }
}

public record RefreshTokenRequest(string RefreshToken);
