using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.DeactivateUser;

public record DeactivateUserCommand(Guid UserId) : ICommand<Result>;
