FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /var/apps/SmartHomeServer

EXPOSE $SMARTHOME_MQTT_PORT
EXPOSE $SMARTHOME_HTTP_PORT
EXPOSE $SMARTHOME_HTTPS_PORT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /var/appsrcs/src
COPY ["SmartHomeServer/SmartHomeServer.csproj","SmartHomeServer/"]
RUN dotnet restore "SmartHomeServer/SmartHomeServer.csproj"
COPY . .
WORKDIR "/var/appsrcs/src/SmartHomeServer"
RUN dotnet build "SmartHomeServer.csproj" -c $BUILD_CONFIGURATION -o /var/apps/SmartHomeServer/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SmartHomeServer.csproj" -c $BUILD_CONFIGURATION -o /var/apps/SmartHomeServer/publish

FROM base AS final
WORKDIR /var/apps/SmartHomeServer
COPY --from=publish /var/apps/SmartHomeServer/publish .
ENTRYPOINT dotnet SmartHomeServer.dll