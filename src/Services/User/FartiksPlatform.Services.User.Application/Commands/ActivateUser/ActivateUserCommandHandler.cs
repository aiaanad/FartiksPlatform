using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Application.Errors;
using FartiksPlatform.Services.User.Domain.Entities;

namespace FartiksPlatform.Services.User.Application.Commands.ActivateUser;

public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, Result>
{
    private readonly IUserUnitOfWork _unitOfWork;

    public ActivateUserCommandHandler(IUserUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        user.Activate();
        _unitOfWork.Users.UpdateUser(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
