using FartiksPlatform.Services.Gameplay.Infrastructure.Persistence;
using FartiksPlatform.Services.Gameplay.Infrastructure.Persistence.Repositories;
using FartiksPlatform.Services.Gameplay.Infrastructure.Services;
using FartiksPlatform.Services.Gameplay.Domain.Repositories;
using FartiksPlatform.Services.Gameplay.Application.Abstractions;
using FartiksPlatform.Services.Gameplay.Domain.Abstractions;
using FartiksPlatform.BuildingBlocks.Common;
using BuildingBlocks.Grpc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FartiksPlatform.Services.Gameplay.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<GameplayDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                sql =>
                {
                    sql.MigrationsAssembly(typeof(GameplayDbContext).Assembly.FullName);
                });
        });

        // Repositories
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGameRoundRepository, GameRoundRepository>();

        // Unit of Work
        services.AddScoped<IGameplayUnitOfWork, GameplayUnitOfWork>();

        // Application Services
        services.AddScoped<IRandomProvider, CryptoRandomProvider>();

        // Grpc
        services.AddGrpcClient<BillingGrpcService.BillingGrpcServiceClient>(options =>
        {
            options.Address = new Uri(configuration["Grpc:BillingUrl"] ?? "http://fartiks-billing-service:8080");
        });
    }
}
