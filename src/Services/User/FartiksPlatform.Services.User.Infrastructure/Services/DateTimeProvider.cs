using FartiksPlatform.Services.User.Application.Interfaces;

namespace FartiksPlatform.Services.User.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => throw new NotImplementedException();

    public DateTime Now => throw new NotImplementedException();
}
