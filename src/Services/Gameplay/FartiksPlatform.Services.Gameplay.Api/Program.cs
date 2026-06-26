using FartiksPlatform.Services.Gameplay.Application.UseCases.GetGames;  
  
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);  
  
builder.Services.AddControllers();  
builder.Services.AddOpenApi();  
  
// MediatR: регистрируем хендлеры из Application-сборки (где лежит GetGamesQuery).  
builder.Services.AddMediatR(cfg =>  
    cfg.RegisterServicesFromAssembly(typeof(GetGamesQuery).Assembly));  
  
// TODO: при необходимости подключить реальный Infrastructure/DI Gameplay  
// (текущий DependencyInjection.cs в Gameplay.Infrastructure содержит регистрации Billing).  
  
WebApplication app = builder.Build();  
  
app.MapOpenApi();  
app.UseRouting();  
app.MapControllers();  
  
app.Run();
