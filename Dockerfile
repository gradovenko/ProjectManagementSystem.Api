FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0-alpine3.9 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-alpine3.9 AS build
WORKDIR /src
COPY ["src/ProjectManagementSystem.WebApi/ProjectManagementSystem.WebApi.csproj", "src/ProjectManagementSystem.WebApi/"]
COPY ["src/ProjectManagementSystem.Domain/ProjectManagementSystem.Domain.csproj", "src/ProjectManagementSystem.Domain/"]
COPY ["src/ProjectManagementSystem.Queries.Infrastructure/ProjectManagementSystem.Queries.Infrastructure.csproj", "src/ProjectManagementSystem.Queries.Infrastructure/"]
COPY ["src/ProjectManagementSystem.Queries/ProjectManagementSystem.Queries.csproj", "src/ProjectManagementSystem.Queries/"]
COPY ["src/ProjectManagementSystem.Infrastructure/ProjectManagementSystem.Infrastructure.csproj", "src/ProjectManagementSystem.Infrastructure/"]
COPY ["src/ProjectManagementSystem.DatabaseMigrations/ProjectManagementSystem.DatabaseMigrations.csproj", "src/ProjectManagementSystem.DatabaseMigrations/"]
RUN dotnet restore "src/ProjectManagementSystem.WebApi/ProjectManagementSystem.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ProjectManagementSystem.WebApi"
RUN dotnet build "ProjectManagementSystem.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectManagementSystem.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectManagementSystem.WebApi.dll"]