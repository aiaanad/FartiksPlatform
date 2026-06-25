namespace FartiksPlatform.Services.User.Api.Contracts;

public record LoginUserResponse(
    Guid UserId,
    string Username,
    string Email,
    string Token,
    string RefreshToken,
    DateTime ExpiresAt);