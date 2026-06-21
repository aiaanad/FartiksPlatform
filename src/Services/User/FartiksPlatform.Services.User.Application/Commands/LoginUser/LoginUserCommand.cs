using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.LoginUser;

public record LoginUserCommand(
    string Email,
    string Password) : ICommand<Result<LoginUserResponse>>;

public record LoginUserResponse(
    Guid UserId,
    string Username,
    string Email,
    string Token,
    string RefreshToken);
