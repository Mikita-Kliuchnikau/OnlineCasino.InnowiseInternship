FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/GamingService/src/GamingService.OutboxWorker/GamingService.OutboxWorker.csproj", "src/Services/GamingService/src/GamingService.OutboxWorker/"]
COPY ["src/Services/GamingService/src/GamingService.OutboxWorker/nuget.config", "src/Services/GamingService/src/GamingService.OutboxWorker/"]
COPY ["src/Services/GamingService/src/GamingService.OutboxWorker/appsettings.Development.json", "src/Services/GamingService/src/GamingService.OutboxWorker/"]
RUN dotnet restore "./src/Services/GamingService/src/GamingService.OutboxWorker/GamingService.OutboxWorker.csproj"
COPY . .
WORKDIR "/src/src/Services/GamingService/src/GamingService.OutboxWorker"
RUN dotnet build "./GamingService.OutboxWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GamingService.OutboxWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamingService.OutboxWorker.dll"]