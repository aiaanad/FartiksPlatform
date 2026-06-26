using System.Text;  
using System.Text.Json;  
using System.Text.Json.Nodes;  
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.IdentityModel.Tokens;  
  
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);  
  
var jwtSection = builder.Configuration.GetSection("Jwt");  
  
builder.Services  
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
    .AddJwtBearer(options =>  
    {  
        options.TokenValidationParameters = new TokenValidationParameters  
        {  
            ValidateIssuer = true,  
            ValidateAudience = true,  
            ValidateLifetime = true,  
            ValidateIssuerSigningKey = true,  
            ValidIssuer = jwtSection["Issuer"],  
            ValidAudience = jwtSection["Audience"],  
            IssuerSigningKey = new SymmetricSecurityKey(  
                Encoding.UTF8.GetBytes(jwtSection["Secret"] ?? string.Empty))  
        };  
    });  
  
builder.Services.AddAuthorization();  
builder.Services.AddHttpClient();  
  
builder.Services  
    .AddReverseProxy()  
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));  
  
WebApplication app = builder.Build();  
  
app.UseAuthentication();  
app.UseAuthorization();  
  
// Маппинг: имя сервиса -> (внутренний адрес сервиса, путь к его спеке, gateway-префикс для Try it out)  
var specs = new (string Name, string ClusterKey, string SpecPath, string GatewayPrefix)[]  
{  
    ("user",     "user-service",     "/openapi/v1.json", "/user"),  
    ("billing",  "billing-service",  "/openapi/v1.json", "/billing"),  
    ("gameplay", "gameplay-service", "/openapi/v1.json", "/gameplay"),  
};  
  
// Эндпоинт, который тянет спеку у сервиса, переписывает servers на gateway-префикс и отдаёт JSON.  
foreach (var spec in specs)  
{  
    string address = builder.Configuration[  
        $"ReverseProxy:Clusters:{spec.ClusterKey}:Destinations:d1:Address"]!;  
  
    app.MapGet($"/openapi/{spec.Name}/v1.json",  
        async (IHttpClientFactory httpClientFactory, CancellationToken ct) =>  
    {  
        HttpClient client = httpClientFactory.CreateClient();  
        string raw = await client.GetStringAsync(  
            $"{address.TrimEnd('/')}{spec.SpecPath}", ct);  
  
        JsonNode root = JsonNode.Parse(raw)!;  
        // Вариант Б: нативные пути сервиса доступны через catch-all как /<prefix>/<path>,  
        // поэтому достаточно выставить server = gateway-префикс.  
        root["servers"] = new JsonArray(  
            new JsonObject { ["url"] = spec.GatewayPrefix });  
  
        return Results.Text(root.ToJsonString(  
            new JsonSerializerOptions { WriteIndented = false }),  
            "application/json");  
    }).AllowAnonymous();  
}  
  
app.UseSwaggerUI(c =>  
{  
    c.SwaggerEndpoint("/openapi/user/v1.json", "User API");  
    c.SwaggerEndpoint("/openapi/billing/v1.json", "Billing API");  
    c.SwaggerEndpoint("/openapi/gameplay/v1.json", "Gameplay API");  
    c.RoutePrefix = "swagger";  
});  
  
app.MapGet("/", () => Results.Ok("Gateway is running"));  
app.MapReverseProxy();  
  
app.Run();
