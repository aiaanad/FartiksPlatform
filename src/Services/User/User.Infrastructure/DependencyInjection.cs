using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces;
using User.Infrastructure.Messaging;

namespace User.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();
        
        return services;
    }
}
