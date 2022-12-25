FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY "BusAggregator.sln" "BusAggregator.sln"

COPY "ApiGateways/Gateway.BaseApi/Gateway.BaseApi.csproj" "ApiGateways/Gateway.BaseApi/Gateway.BaseApi.csproj"
COPY "Services/Adapter/Adapter.API/Adapter.API.csproj" "Services/Adapter/Adapter.API/Adapter.API.csproj"
COPY "Services/Catalog/Catalog.API/Catalog.API.csproj" "Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY "WebStatus/WebStatus.csproj" "WebStatus/WebStatus.csproj"

COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "BusAggregator.sln"

COPY . .
WORKDIR /Services/Adapter/Adapter.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Adapter.API.dll"]