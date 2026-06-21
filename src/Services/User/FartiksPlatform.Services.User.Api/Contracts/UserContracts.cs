namespace FartiksPlatform.Services.User.Api.Contracts;

public record RegisterUserRequest(string Username, string Email, string Password, string Role);

public record RegisterUserResponse(Guid UserId, string Message);

public record LoginUserRequest(string Email, string Password);

public record LoginUserResponse(
    Guid UserId,
    string Username,
    string Email,
    string Token,
    string RefreshToken,
    DateTime ExpiresAt);

public record VerifyEmailRequest(string VerificationCode);

public record UserProfileResponse(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt,
    bool EmailVerified);

public record UsersPagedResponse(
    IReadOnlyList<UserItemDto> Users,
    int TotalCount,
    int Page,
    int PageSize);

public record UserItemDto(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt);
