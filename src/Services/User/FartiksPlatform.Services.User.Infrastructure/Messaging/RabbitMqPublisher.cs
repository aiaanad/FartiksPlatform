using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using FartiksPlatform.Services.User.Application.Interfaces;

namespace FartiksPlatform.Services.User.Infrastructure.Messaging;

public class RabbitMqPublisher(IConfiguration configuration) : IEventPublisher, IAsyncDisposable
{
    private readonly ConnectionFactory _factory = new()
    {
        HostName = configuration["RabbitMQ:Host"] ?? "localhost",
        UserName = configuration["RabbitMQ:Username"] ?? "guest",
        Password = configuration["RabbitMQ:Password"] ?? "guest",
    };
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly SemaphoreSlim _lock = new(1, 1);

    private async Task<IChannel> GetChannelAsync()
    {
        await _lock.WaitAsync();
        try
        {
            if (_channel is { IsOpen: true })
                return _channel;

            _connection = await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            return _channel;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task PublishAsync<T>(T @event, string exchangeName, string routingKey)
    {
        IChannel channel = await GetChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true);

        string json = JsonSerializer.Serialize(@event);
        byte[] body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            await _channel.CloseAsync();
            _channel.Dispose();
        }

        if (_connection is not null)
        {
            await _connection.CloseAsync();
            _connection.Dispose();
        }

        _lock.Dispose();
    }
}
