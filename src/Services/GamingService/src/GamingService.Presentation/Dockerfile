FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/GamingService/src/GamingService.Presentation/GamingService.Presentation.csproj", "src/Services/GamingService/src/GamingService.Presentation/"]
COPY ["src/Services/GamingService/src/GamingService.Application/GamingService.Application.csproj", "src/Services/GamingService/src/GamingService.Application/"]
COPY ["src/Services/GamingService/src/GamingService.Core/GamingService.Core.csproj", "src/Services/GamingService/src/GamingService.Core/"]
RUN dotnet restore "./src/Services/GamingService/src/GamingService.Presentation/GamingService.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/GamingService/src/GamingService.Presentation"
RUN dotnet build "./GamingService.Presentation.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GamingService.Presentation.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamingService.Presentation.dll"]