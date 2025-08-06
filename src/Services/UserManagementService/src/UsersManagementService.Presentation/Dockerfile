FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/UserManagementService/src/UsersManagementService.Presentation/UsersManagementService.Presentation.csproj", "src/Services/UserManagementService/src/UsersManagementService.Presentation/"]
COPY ["src/Services/UserManagementService/src/UsersManagementService.BLL/UsersManagementService.BLL.csproj", "src/Services/UserManagementService/src/UsersManagementService.BLL/"]
COPY ["src/Services/UserManagementService/src/UsersManagementService.Common/UsersManagementService.Common.csproj", "src/Services/UserManagementService/src/UsersManagementService.Common/"]
COPY ["src/Services/UserManagementService/src/UsersManagementService.DAL/UsersManagementService.DAL.csproj", "src/Services/UserManagementService/src/UsersManagementService.DAL/"]
COPY ["src/Services/UserManagementService/src/UsersManagementService.Presentation/nuget.config", "src/Services/UserManagementService/src/UsersManagementService.Presentation/"]
RUN dotnet restore "./src/Services/UserManagementService/src/UsersManagementService.Presentation/UsersManagementService.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Services/UserManagementService/src/UsersManagementService.Presentation"
RUN dotnet build "./UsersManagementService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UsersManagementService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersManagementService.Presentation.dll"]