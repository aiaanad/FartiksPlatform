using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Application.Errors;

namespace FartiksPlatform.Services.User.Application.Commands.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IUserUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.AppUser? user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        await _unitOfWork.Users.DeleteUserAsync(request.UserId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
