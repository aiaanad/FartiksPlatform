using Billing.Application.Interfaces;
using Billing.Application.Services;
using Billing.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<UserRegisteredConsumerService>();
        
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
}
