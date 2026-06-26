using System.Text;
using System.Text.Json;
using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.BuildingBlocks.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FartiksPlatform.Services.Billing.Infrastructure.Messaging;

public class UserRegisteredConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection = null!;
    private IChannel _channel = null!;
    private readonly ILogger<UserRegisteredConsumerService> _logger;
    private readonly IConfiguration _configuration;
    
    private const string ExchangeName = "user-exchange";
    private const string QueueName = "billing-user-registered-queue";
    private const string RoutingKey = "user.registered";

    public UserRegisteredConsumerService(
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        ILogger<UserRegisteredConsumerService> logger)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        string host = _configuration["RabbitMQ:Host"] ?? "localhost";
        string user = _configuration["RabbitMQ:Username"] ?? "guest";
        string pass = _configuration["RabbitMQ:Password"] ?? "guest";

        var factory = new ConnectionFactory
        {
            HostName = host,
            UserName = user,
            Password = pass,
            AutomaticRecoveryEnabled = true
        };

        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.ExchangeDeclareAsync(
            exchange: ExchangeName, 
            type: ExchangeType.Direct, 
            durable: true,
            cancellationToken: stoppingToken);
        
        await _channel.QueueDeclareAsync(
            queue: QueueName, 
            durable: true, 
            exclusive: false, 
            autoDelete: false,
            cancellationToken: stoppingToken);
        
        await _channel.QueueBindAsync(
            queue: QueueName, 
            exchange: ExchangeName, 
            routingKey: RoutingKey,
            cancellationToken: stoppingToken);
        
        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
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

                await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Billing Service] Error while processing message. {Message}", ex.Message);
                await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(queue: QueueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync(cancellationToken: cancellationToken);
        await _connection.CloseAsync(cancellationToken: cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}
