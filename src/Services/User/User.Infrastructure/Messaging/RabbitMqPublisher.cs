using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using User.Application.Interfaces;

namespace User.Infrastructure.Messaging;

public class RabbitMqPublisher : IEventPublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    
    public RabbitMqPublisher(IConfiguration configuration)
    {
        string host = configuration["RabbitMQ:Host"] ?? "localhost";
        string user = configuration["RabbitMQ:Username"] ?? "guest";
        string pass = configuration["RabbitMQ:Password"] ?? "guest";

        var factory = new ConnectionFactory
        {
            HostName = host,
            UserName = user,
            Password = pass,
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    
    public void Publish<T>(T @event, string exchangeName, string routingKey)
    {
        _channel.ExchangeDeclare(
            exchange: exchangeName, 
            type: ExchangeType.Direct, 
            durable: true);
        
        string json = JsonSerializer.Serialize(@event);
        byte[] body = Encoding.UTF8.GetBytes(json);

        IBasicProperties? properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        
        _channel.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
