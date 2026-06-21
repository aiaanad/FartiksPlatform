FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/
COPY src/Services/Gameplay/Gameplay.Api/Gameplay.Api.csproj src/Services/Gameplay/Gameplay.Api/
COPY src/Services/Gameplay/Gameplay.Application/Gameplay.Application.csproj src/Services/Gameplay/Gameplay.Application/
COPY src/Services/Gameplay/Gameplay.Domain/Gameplay.Domain.csproj src/Services/Gameplay/Gameplay.Domain/
COPY src/Services/Gameplay/Gameplay.Infrastructure/Gameplay.Infrastructure.csproj src/Services/Gameplay/Gameplay.Infrastructure/

RUN dotnet restore src/Services/Gameplay/Gameplay.Api/Gameplay.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/Gameplay/ src/Services/Gameplay/

WORKDIR /app/src/Services/Gameplay/Gameplay.Api
RUN dotnet publish Gameplay.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Gameplay.Api.dll"]