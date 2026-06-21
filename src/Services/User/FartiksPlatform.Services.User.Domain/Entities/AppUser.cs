using FartiksPlatform.Services.User.Domain.Enums;
using FartiksPlatform.Services.User.Domain.ValueObjects;

namespace FartiksPlatform.Services.User.Domain.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool EmailVerified { get; set; }

    public static AppUser Create(
        Guid id,
        string username,
        Email email,
        string passwordHash,
        string role)
    {
        throw new NotImplementedException();
    }

    public void UpdateProfile(string username, Email email)
    {
        throw new NotImplementedException();
    }

    public void VerifyEmail()
    {
        throw new NotImplementedException();
    }

    public void ChangePassword(string newPasswordHash)
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }

    public void Activate()
    {
        throw new NotImplementedException();
    }
}
