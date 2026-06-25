namespace FartiksPlatform.Services.User.Api.Contracts;

public record RegisterUserRequest(string Username, string Email, string Password, string Role);
