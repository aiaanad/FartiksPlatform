FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/FartiksPlatform.BuildingBlocks/FartiksPlatform.BuildingBlocks.csproj src/FartiksPlatform.BuildingBlocks/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/FartiksPlatform.Services.Gameplay.Api.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Application/FartiksPlatform.Services.Gameplay.Application.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Application/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Domain/FartiksPlatform.Services.Gameplay.Domain.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Domain/
COPY src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Infrastructure/FartiksPlatform.Services.Gameplay.Infrastructure.csproj src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Infrastructure/

RUN dotnet restore src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api/FartiksPlatform.Services.Gameplay.Api.csproj

COPY src/FartiksPlatform.BuildingBlocks/ src/FartiksPlatform.BuildingBlocks/
COPY src/Services/Gameplay/ src/Services/Gameplay/

WORKDIR /app/src/Services/Gameplay/FartiksPlatform.Services.Gameplay.Api
RUN dotnet publish FartiksPlatform.Services.Gameplay.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FartiksPlatform.Services.Gameplay.Api.dll"]