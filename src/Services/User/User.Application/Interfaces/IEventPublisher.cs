using BuildingBlocks.Events;

namespace User.Application.Interfaces;

public interface IEventPublisher
{
    void Publish<T>(T @event, string exchangeName, string routingKey);
}
