using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Commands.VerifyEmail;

public record VerifyEmailCommand(Guid UserId, string VerificationCode) : ICommand<Result>;
