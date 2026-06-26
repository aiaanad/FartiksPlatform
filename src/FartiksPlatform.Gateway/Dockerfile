FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Directory.Build.props ./
COPY src/FartiksPlatform.Gateway/FartiksPlatform.Gateway.csproj src/FartiksPlatform.Gateway/

RUN dotnet restore src/FartiksPlatform.Gateway/FartiksPlatform.Gateway.csproj

COPY . .
RUN dotnet publish src/FartiksPlatform.Gateway/FartiksPlatform.Gateway.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FartiksPlatform.Gateway.dll"]
