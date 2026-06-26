using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Application.Services;
using FartiksPlatform.Services.Billing.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FartiksPlatform.Services.Billing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<UserRegisteredConsumerService>();
        
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
}
