using FartiksPlatform.Services.User.Application.Commands.RegisterUser;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Messaging;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Configurations;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Security;
using FartiksPlatform.Services.User.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FartiksPlatform.Services.User.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<UserDbContext>(options =>
        {
            throw new NotImplementedException();
        });

        services.AddScoped<IUserDbContext>(provider =>
        {
            return provider.GetRequiredService<UserDbContext>();
        });

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Application Services
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHashGenerator, PasswordHashGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        // Options
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddSingleton(provider =>
        {
            return provider.GetRequiredService<IOptions<JwtOptions>>().Value;
        });

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
        });

        // MassTransit
        MassTransitBusConfig.ConfigureMassTransit(services, configuration);
    }
}
