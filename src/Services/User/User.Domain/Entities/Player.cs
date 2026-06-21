using User.Domain.Enums;
using User.Domain.ValueObjects;

namespace User.Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
