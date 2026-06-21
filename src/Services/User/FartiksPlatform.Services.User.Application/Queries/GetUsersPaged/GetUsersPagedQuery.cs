using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Queries.GetUsersPaged;

public record GetUsersPagedQuery(int Page, int PageSize) : IQuery<Result<UsersPagedResponse>>;

public record UsersPagedResponse(
    IReadOnlyList<UserDto> Users,
    int TotalCount,
    int Page,
    int PageSize);

public record UserDto(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt);
