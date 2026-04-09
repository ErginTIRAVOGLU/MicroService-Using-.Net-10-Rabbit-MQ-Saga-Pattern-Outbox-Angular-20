FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5050

ENV ASPNETCORE_URLS=http://+:5050

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Payment/Payment.csproj", "Services/Payment/"]
COPY ["Infrastructure/Common.Logging/Common.Logging.csproj", "Infrastructure/Common.Logging/"]
COPY ["Infrastructure/EventBus.Messages/EventBus.Messages.csproj", "Infrastructure/EventBus.Messages/"]
RUN dotnet restore "Services/Payment/Payment.csproj"
COPY . .
WORKDIR "/src/Services/Payment"
RUN dotnet build "Payment.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Payment.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.dll"]
