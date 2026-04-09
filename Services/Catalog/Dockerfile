FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Catalog/Catalog.csproj", "Services/Catalog/"]
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj", "Infrastructure/Common.Logging/"]
RUN dotnet restore "Services/Catalog/Catalog.csproj"
COPY . .
WORKDIR "/src/Services/Catalog"
RUN dotnet build "Catalog.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Catalog.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.dll"]
