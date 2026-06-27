using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand<Result>;
