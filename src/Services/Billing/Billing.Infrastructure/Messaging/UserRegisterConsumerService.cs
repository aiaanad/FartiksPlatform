using System.Text;
using System.Text.Json;
using Billing.Application.Interfaces;
using BuildingBlocks.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Billing.Infrastructure.Messaging;

public class UserRegisteredConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<UserRegisteredConsumerService> _logger;
    
    private const string ExchangeName = "user-exchange";
    private const string QueueName = "billing-user-registered-queue";
    private const string RoutingKey = "user.registered";

    public UserRegisteredConsumerService(
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        ILogger<UserRegisteredConsumerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        string host = configuration["RabbitMQ:Host"] ?? "localhost";
        string user = configuration["RabbitMQ:Username"] ?? "guest";
        string pass = configuration["RabbitMQ:Password"] ?? "guest";

        var factory = new ConnectionFactory
        {
            HostName = host,
            UserName = user,
            Password = pass,
            DispatchConsumersAsync = true 
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(
            exchange: ExchangeName, 
            type: ExchangeType.Direct, 
            durable: true);
        
        _channel.QueueDeclare(
            queue: QueueName, 
            durable: true, 
            exclusive: false, 
            autoDelete: false);
        
        _channel.QueueBind(
            queue: QueueName, 
            exchange: ExchangeName, 
            routingKey: RoutingKey);
        
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            
            try
            {
                UserRegisteredEvent? @event = JsonSerializer.Deserialize<UserRegisteredEvent>(message);
                if (@event is not null)
                {
                    _logger.LogInformation("[Billing Service] A message has been received from the queue. PlayerId: {PlayerId}", @event.PlayerId);

                    using IServiceScope scope = _serviceProvider.CreateScope();
                    IWalletService walletService = scope.ServiceProvider.GetRequiredService<IWalletService>();
                    await walletService.CreateWalletAsync(@event.PlayerId, stoppingToken);
                }

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Billing Service] Error while processing message. {ex.Message}");
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
                // Потом надо поменять requeue на true, чтобы сообщения обратно закидывались в очередь
            }
        };

        _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }
    
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
