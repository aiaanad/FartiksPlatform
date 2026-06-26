namespace FartiksPlatform.BuildingBlocks.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
    DateTime Now { get; }
}
