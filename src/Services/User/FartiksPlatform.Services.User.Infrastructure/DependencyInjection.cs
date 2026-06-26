using FartiksPlatform.Services.User.Application.Abstractions.Persistence;
using FartiksPlatform.Services.User.Application.Commands.RegisterUser;
using FartiksPlatform.Services.User.Application.Interfaces;
using FartiksPlatform.Services.User.Domain.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Messaging;
using FartiksPlatform.Services.User.Infrastructure.Persistence;
using FartiksPlatform.Services.User.Infrastructure.Persistence.Repositories;
using FartiksPlatform.Services.User.Infrastructure.Security;
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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                sql =>
                {
                    sql.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
                });
        });

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Unit of Work
        services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

        // Application Services
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHashGenerator, PasswordHashGenerator>();

        // Messaging
        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();

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
    }
}
