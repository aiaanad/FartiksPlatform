using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Application.Errors;
using FartiksPlatform.Services.User.Domain.Repositories;

namespace FartiksPlatform.Services.User.Application.Queries.GetUserProfile;

public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, Result<UserProfileResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUserProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Result<UserProfileResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.AppUser? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);

        var response = new UserProfileResponse(
            user.Id,
            user.Username,
            user.Email.Value,
            user.Role,
            user.Status,
            user.CreatedAt,
            user.EmailVerified);

        return Result.Success(response);
    }
}
