FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/FartiksPlatform.BuildingBlocks/FartiksPlatform.BuildingBlocks.csproj src/FartiksPlatform.BuildingBlocks/

COPY src/Services/Billing/FartiksPlatform.Services.Billing.Application/FartiksPlatform.Services.Billing.Application.csproj src/Services/Billing/FartiksPlatform.Services.Billing.Application/

COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Domain/FartiksPlatform.Services.Gameplay.Domain.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Domain/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Application/FartiksPlatform.Services.Gameplay.Application.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Application/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Infrastructure/FartiksPlatform.Services.Gameplay.Infrastructure.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Infrastructure/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/FartiksPlatform.Services.Gameplay.Api.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/

RUN dotnet restore src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/FartiksPlatform.Services.Gameplay.Api.csproj

COPY . .

RUN dotnet publish src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/FartiksPlatform.Services.Gameplay.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FartiksPlatform.Services.Gameplay.Api.dll"]
