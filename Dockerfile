FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/ProjectManagementSystem.WebApi/*.csproj ./ProjectManagementSystem.WebApi/
RUN dotnet restore

# copy everything else and build app
COPY ProjectManagementSystem.WebApi/. ./ProjectManagementSystem.WebApi/
WORKDIR /app/ProjectManagementSystem.WebApi
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/ProjectManagementSystem.WebApi/out ./
ENTRYPOINT ["dotnet", "ProjectManagementSystem.WebApi.dll"]
