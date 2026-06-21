FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/FartiksPlatform.BuildingBlocks/FartiksPlatform.BuildingBlocks.csproj src/FartiksPlatform.BuildingBlocks/
COPY src/Services/User/FartiksPlatform.Services.User.Api/FartiksPlatform.Services.User.Api.csproj src/Services/User/FartiksPlatform.Services.User.Api/
COPY src/Services/User/FartiksPlatform.Services.User.Application/FartiksPlatform.Services.User.Application.csproj src/Services/User/FartiksPlatform.Services.User.Application/
COPY src/Services/User/FartiksPlatform.Services.User.Domain/FartiksPlatform.Services.User.Domain.csproj src/Services/User/FartiksPlatform.Services.User.Domain/
COPY src/Services/User/FartiksPlatform.Services.User.Infrastructure/FartiksPlatform.Services.User.Infrastructure.csproj src/Services/User/FartiksPlatform.Services.User.Infrastructure/

RUN dotnet restore src/Services/User/FartiksPlatform.Services.User.Api/FartiksPlatform.Services.User.Api.csproj

COPY src/FartiksPlatform.BuildingBlocks/ src/FartiksPlatform.BuildingBlocks/
COPY src/Services/User/ src/Services/User/

WORKDIR /app/src/Services/User/FartiksPlatform.Services.User.Api
RUN dotnet publish FartiksPlatform.Services.User.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FartiksPlatform.Services.User.Api.dll"]