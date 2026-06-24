using FartiksPlatform.Services.User.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FartiksPlatform.Services.User.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddCors(options =>
        {
            throw new NotImplementedException();
        });

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                throw new NotImplementedException();
            });

        builder.Services.AddAuthorization();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
