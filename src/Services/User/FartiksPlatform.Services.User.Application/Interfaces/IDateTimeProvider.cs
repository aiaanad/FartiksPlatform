namespace FartiksPlatform.Services.User.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
}
