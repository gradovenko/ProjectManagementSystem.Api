FROM mcr.microsoft.com/dotnet/aspnet:7.0.5-alpine3.17-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0.5-alpine3.17-amd64 AS build
WORKDIR /src
COPY ["src/ProjectManagementSystem.Api/ProjectManagementSystem.Api.csproj", "src/ProjectManagementSystem.Api/"]
COPY ["src/ProjectManagementSystem.Domain/ProjectManagementSystem.Domain.csproj", "src/ProjectManagementSystem.Domain/"]
COPY ["src/ProjectManagementSystem.Queries.Infrastructure/ProjectManagementSystem.Queries.Infrastructure.csproj", "src/ProjectManagementSystem.Queries.Infrastructure/"]
COPY ["src/ProjectManagementSystem.Queries/ProjectManagementSystem.Queries.csproj", "src/ProjectManagementSystem.Queries/"]
COPY ["src/ProjectManagementSystem.Infrastructure/ProjectManagementSystem.Infrastructure.csproj", "src/ProjectManagementSystem.Infrastructure/"]
COPY ["src/ProjectManagementSystem.DatabaseMigrations/ProjectManagementSystem.DatabaseMigrations.csproj", "src/ProjectManagementSystem.DatabaseMigrations/"]
RUN dotnet restore "src/ProjectManagementSystem.Api/ProjectManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/src/ProjectManagementSystem.Api"
RUN dotnet build "ProjectManagementSystem.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectManagementSystem.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectManagementSystem.Api.dll"]