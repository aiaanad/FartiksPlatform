FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

COPY FartiksPlatform.slnx ./
COPY Directory.Build.props ./

COPY src/BuildingBlocks/BuildingBlocks.csproj src/BuildingBlocks/
COPY src/Services/Billing/Billing.Api/Billing.Api.csproj src/Services/Billing/Billing.Api/
COPY src/Services/Billing/Billing.Application/Billing.Application.csproj src/Services/Billing/Billing.Application/
COPY src/Services/Billing/Billing.Domain/Billing.Domain.csproj src/Services/Billing/Billing.Domain/
COPY src/Services/Billing/Billing.Infrastructure/Billing.Infrastructure.csproj src/Services/Billing/Billing.Infrastructure/

RUN dotnet restore src/Services/Billing/Billing.Api/Billing.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/Billing/ src/Services/Billing/

WORKDIR /app/src/Services/Billing/Billing.Api
RUN dotnet publish Billing.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Billing.Api.dll"]