using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password,
    string Role) : ICommand<Result>;
