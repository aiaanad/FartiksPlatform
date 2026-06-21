using FartiksPlatform.Services.User.Api.Contracts;
using FartiksPlatform.Services.User.Application.Queries.GetUserProfile;
using FartiksPlatform.Services.User.Application.Queries.GetUsersPaged;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FartiksPlatform.Services.User.Api.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class AdminUsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminUsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserProfile([FromRoute] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{userId}/deactivate")]
    public async Task<IActionResult> DeactivateUser([FromRoute] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{userId}/activate")]
    public async Task<IActionResult> ActivateUser([FromRoute] Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        throw new NotImplementedException();
    }
}
