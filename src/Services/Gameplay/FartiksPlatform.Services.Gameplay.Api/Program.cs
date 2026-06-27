using FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;
using FartiksPlatform.Services.Gameplay.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// MediatR: регистрируем хендлеры из Application-сборки (где лежит GetGamesQuery).  
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly);
});

WebApplication app = builder.Build();

app.MapOpenApi();
app.UseRouting();
app.MapControllers();

app.Run();
