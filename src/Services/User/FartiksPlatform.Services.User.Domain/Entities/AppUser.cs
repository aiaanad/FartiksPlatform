using FartiksPlatform.Services.User.Domain.ValueObjects;

namespace FartiksPlatform.Services.User.Domain.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool EmailVerified { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    private AppUser() { }

    public static AppUser Create(
        Guid id,
        string username,
        Email email,
        string passwordHash,
        string role)
    {
        return new AppUser
        {
            Id = id,
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            Status = "Active",
            EmailVerified = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProfile(string username, Email email)
    {
        Username = username;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        EmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = "Inactive";
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = "Active";
        UpdatedAt = DateTime.UtcNow;
    }

    public RefreshToken AddRefreshToken(string tokenHash, DateTime expiresAt, string deviceInfo, string ipAddress)
    {
        var refreshToken = new RefreshToken(
            Id,
            tokenHash,
            expiresAt,
            deviceInfo,
            ipAddress
        );

        _refreshTokens.Add(refreshToken);
        return refreshToken;
    }
}
