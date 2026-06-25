using FartiksPlatform.BuildingBlocks.Events;

namespace FartiksPlatform.Services.User.Application.Interfaces;

public interface IEventPublisher
{
    void Publish<T>(T @event, string exchangeName, string routingKey);
}
