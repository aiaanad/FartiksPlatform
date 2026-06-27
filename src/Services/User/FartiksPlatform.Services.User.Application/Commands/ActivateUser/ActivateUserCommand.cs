using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.ActivateUser;

public record ActivateUserCommand(Guid UserId) : ICommand<Result>;
