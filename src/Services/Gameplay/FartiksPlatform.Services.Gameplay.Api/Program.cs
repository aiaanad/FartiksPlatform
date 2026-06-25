using Microsoft.EntityFrameworkCore;


public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.

        app.Run();
    }
}
