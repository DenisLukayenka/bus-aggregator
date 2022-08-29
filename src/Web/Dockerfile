# Build Web Client
FROM node:16-alpine as web-build
WORKDIR /webbuild/src

COPY Web/Client/package.json .
COPY Web/Client/yarn.lock .

RUN yarn install

COPY Web/Client .
RUN npm run build
# End

# Init Dotnet server
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://*:5002
ENV ASPNETCORE_ENVIRONMENT=Production

# Publish project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src

COPY "BusAggregator.sln" "BusAggregator.sln"

COPY "ApiGateways/Gateway.BaseApi/Gateway.BaseApi.csproj" "ApiGateways/Gateway.BaseApi/Gateway.BaseApi.csproj"
COPY "Services/Adapter/Adapter.API/Adapter.API.csproj" "Services/Adapter/Adapter.API/Adapter.API.csproj"
COPY "Services/Catalog/Catalog.API/Catalog.API.csproj" "Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY "Web/WebSPA.csproj" "Web/WebSPA.csproj"
COPY "WebStatus/WebStatus.csproj" "WebStatus/WebStatus.csproj"

COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "BusAggregator.sln"

COPY . .
COPY --from=web-build /webbuild/wwwroot /src/Web/wwwroot
WORKDIR /src/Web
RUN dotnet publish --no-restore -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebSPA.dll"]

# End publish project
