FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/
COPY src/Services/User/User.Api/User.Api.csproj src/Services/User/User.Api/
COPY src/Services/User/User.Application/User.Application.csproj src/Services/User/User.Application/
COPY src/Services/User/User.Domain/User.Domain.csproj src/Services/User/User.Domain/
COPY src/Services/User/User.Infrastructure/User.Infrastructure.csproj src/Services/User/User.Infrastructure/

RUN dotnet restore src/Services/User/User.Api/User.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/User/ src/Services/User/

WORKDIR /app/src/Services/User/User.Api
RUN dotnet publish User.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "User.Api.dll"]