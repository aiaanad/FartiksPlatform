namespace FartiksPlatform.Services.User.Api.Contracts;

public record UserProfileResponse(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt,
    bool EmailVerified);