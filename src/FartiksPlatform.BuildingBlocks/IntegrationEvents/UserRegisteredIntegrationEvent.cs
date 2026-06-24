namespace FartiksPlatform.BuildingBlocks.IntegrationEvents;

public record UserRegisteredIntegrationEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();

    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    public Guid UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;
}
