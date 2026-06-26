using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Commands.ActivateUser;
using FartiksPlatform.Services.User.Application.Commands.DeactivateUser;
using FartiksPlatform.Services.User.Application.Commands.DeleteUser;
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
        Result<UserProfileResponse> result = await _mediator.Send(new GetUserProfileQuery(userId));
        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        Result<UsersPagedResponse> result = await _mediator.Send(new GetUsersPagedQuery(page, pageSize));
        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPut("{userId}/deactivate")]
    public async Task<IActionResult> DeactivateUser([FromRoute] Guid userId)
    {
        Result result = await _mediator.Send(new DeactivateUserCommand(userId));
        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }

    [HttpPut("{userId}/activate")]
    public async Task<IActionResult> ActivateUser([FromRoute] Guid userId)
    {
        Result result = await _mediator.Send(new ActivateUserCommand(userId));
        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        Result result = await _mediator.Send(new DeleteUserCommand(userId));
        return result.IsSuccess ? NoContent() : result.ToActionResult();
    }
}
