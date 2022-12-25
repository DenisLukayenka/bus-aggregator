# Build Web Client
FROM node:16-alpine as web-build
WORKDIR /webbuild/src

COPY Web/src/Web.SPA/Client/package.json .
COPY Web/src/Web.SPA/Client/yarn.lock .

RUN yarn install

COPY Web/src/Web.SPA/Client .
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

COPY "Web/src/Web.sln" "Web/src/Web.sln"

COPY "Web/src/Web.SPA/Web.SPA.csproj" "Web/src/Web.SPA/Web.SPA.csproj"
COPY "Web/src/Web.Services/Web.Services.csproj" "Web/src/Web.Services/Web.Services.csproj"
COPY "Web/src/Web.Infrastructure/Web.Infrastructure.csproj" "Web/src/Web.Infrastructure/Web.Infrastructure.csproj"
COPY "Web/src/Web.Configuration/Web.Configuration.csproj" "Web/src/Web.Configuration/Web.Configuration.csproj"
COPY "Web/src/Web.SqlMigration/Web.SqlMigration.csproj" "Web/src/Web.SqlMigration/Web.SqlMigration.csproj"

COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "Web/src/Web.sln"

COPY . .
COPY --from=web-build /webbuild/wwwroot /src/Web/src/Web.SPA/wwwroot
WORKDIR /src/Web/src/Web.SPA
RUN dotnet publish --no-restore -c Release -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.SPA.dll"]

# End publish project
