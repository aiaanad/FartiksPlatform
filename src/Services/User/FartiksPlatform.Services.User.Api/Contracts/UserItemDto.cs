namespace FartiksPlatform.Services.User.Api.Contracts;

public record UserItemDto(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string Status,
    DateTime CreatedAt);
