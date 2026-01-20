FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/AutoHub.WebHost/AutoHub.WebHost.csproj", "Presentation/AutoHub.WebHost/"]
RUN dotnet restore "Presentation/AutoHub.WebHost/AutoHub.WebHost.csproj"
COPY . .
WORKDIR "/src/Presentation/AutoHub.WebHost"
RUN dotnet build "./AutoHub.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AutoHub.WebHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoHub.WebHost.dll"]
