using FartiksPlatform.BuildingBlocks.Common;
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
        throw new NotImplementedException();
    }
}
