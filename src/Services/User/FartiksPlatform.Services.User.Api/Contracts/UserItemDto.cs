namespace FartiksPlatform.Services.User.Domain.Constants;

public record UserItemDto(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt);