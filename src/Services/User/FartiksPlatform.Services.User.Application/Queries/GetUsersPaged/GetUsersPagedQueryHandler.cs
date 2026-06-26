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
        (IReadOnlyList<Domain.Entities.AppUser> users, int totalCount) = await _userRepository.GetUsersPagedAsync(request.Page, request.PageSize, cancellationToken);

        var items = users
            .Select(u =>
            {
                return new UserDto(u.Id, u.Username, u.Email.Value, u.Role, u.Status, u.CreatedAt);
            })
            .ToList();

        return Result.Success(new UsersPagedResponse(items, totalCount, request.Page, request.PageSize));
    }
}
