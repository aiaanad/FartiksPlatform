using FartiksPlatform.BuildingBlocks.Common;

namespace FartiksPlatform.Services.User.Application.Queries.GetUserProfile;

public record GetUserProfileQuery(Guid UserId) : IQuery<Result<UserProfileResponse>>;

public record UserProfileResponse(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt,
    bool EmailVerified);
