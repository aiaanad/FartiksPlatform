namespace FartiksPlatform.Services.User.Api.Contracts;

public record UsersPagedResponse(
    IReadOnlyList<UserItemDto> Users,
    int TotalCount,
    int Page,
    int PageSize);
