using FartiksPlatform.BuildingBlocks.Common;
using FartiksPlatform.Services.User.Domain.Repositories;

namespace FartiksPlatform.Services.User.Application.Queries.GetUsersPaged;

public class GetUsersPagedQueryHandler : IQueryHandler<GetUsersPagedQuery, Result<UsersPagedResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersPagedQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Result<UsersPagedResponse>> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
