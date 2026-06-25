namespace FartiksPlatform.Services.User.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? RevokedReason { get; private set; }
    public string? DeviceInfo { get; private set; }
    public string? IpAddress { get; private set; }
    
    public virtual User User { get; private set; }
    
    public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
    
    private RefreshToken() { }
    
    public RefreshToken(Guid userId, string token, DateTime expiresAt, string? deviceInfo = null, string? ipAddress = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
        DeviceInfo = deviceInfo;
        IpAddress = ipAddress;
    }
    
    public void Revoke(string reason)
    {
        RevokedAt = DateTime.UtcNow;
        RevokedReason = reason;
    }
}