using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Application.Errors;

namespace FartiksPlatform.Services.User.Application.Commands.VerifyEmail;

public class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, Result>
{
    private readonly IUserUnitOfWork _unitOfWork;

    public VerifyEmailCommandHandler(IUserUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.AppUser? user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        user.VerifyEmail();
        _unitOfWork.Users.UpdateUser(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
