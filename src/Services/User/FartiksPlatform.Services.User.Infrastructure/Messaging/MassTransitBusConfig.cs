using FartiksPlatform.BuildingBlocks.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FartiksPlatform.Services.User.Infrastructure.Messaging;

public static class MassTransitBusConfig
{
    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}
