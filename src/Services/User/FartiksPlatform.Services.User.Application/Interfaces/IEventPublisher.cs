using FartiksPlatform.BuildingBlocks.Events;

namespace FartiksPlatform.Services.User.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, string exchangeName, string routingKey);
}
