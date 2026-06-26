FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/FartiksPlatform.BuildingBlocks/FartiksPlatform.BuildingBlocks.csproj src/FartiksPlatform.BuildingBlocks/
COPY src/Services/Billing/FartiksPlatform.Services.Billing.Api/FartiksPlatform.Services.Billing.Api.csproj src/Services/Billing/FartiksPlatform.Services.Billing.Api/
COPY src/Services/Billing/FartiksPlatform.Services.Billing.Application/FartiksPlatform.Services.Billing.Application.csproj src/Services/Billing/FartiksPlatform.Services.Billing.Application/
COPY src/Services/Billing/FartiksPlatform.Services.Billing.Domain/FartiksPlatform.Services.Billing.Domain.csproj src/Services/Billing/FartiksPlatform.Services.Billing.Domain/
COPY src/Services/Billing/FartiksPlatform.Services.Billing.Infrastructure/FartiksPlatform.Services.Billing.Infrastructure.csproj src/Services/Billing/FartiksPlatform.Services.Billing.Infrastructure/

RUN dotnet restore src/Services/Billing/FartiksPlatform.Services.Billing.Api/FartiksPlatform.Services.Billing.Api.csproj

COPY src/FartiksPlatform.BuildingBlocks/ src/FartiksPlatform.BuildingBlocks/
COPY src/Services/Billing/ src/Services/Billing/

WORKDIR /app/src/Services/Billing/FartiksPlatform.Services.Billing.Api
RUN dotnet publish FartiksPlatform.Services.Billing.Api.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FartiksPlatform.Services.Billing.Api.dll"]