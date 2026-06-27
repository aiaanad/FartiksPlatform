using FartiksPlatform.Services.Billing.Infrastructure.Persistence.Repositories;
using FartiksPlatform.Services.Billing.Application.Interfaces;
using FartiksPlatform.Services.Billing.Application.Consumers;
using FartiksPlatform.Services.Billing.Api.Grpc;
using FartiksPlatform.BuildingBlocks.Errors;
using FartiksPlatform.Services.Billing.Application.Mappers;
using FartiksPlatform.Services.Billing.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using MassTransit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BillingDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddGrpc();
builder.Services.AddSingleton<IErrorMapper, BillingErrorMapper>();
builder.Services.AddSingleton<IErrorMapper, DefaultErrorMapper>();

/*
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserRegisteredConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"] ?? "localhost");
        cfg.ReceiveEndpoint("billing-user-registered", e =>
        {
            e.ConfigureConsumer<UserRegisteredConsumer>(context);
        });
    });
});
*/

builder.Services.AddControllers();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    BillingDbContext context = scope.ServiceProvider.GetRequiredService<BillingDbContext>();
    context.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapControllers();
app.MapGrpcService<BillingService>();

app.Run();
