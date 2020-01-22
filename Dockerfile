FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10
COPY ./build /app
WORKDIR /app
ENTRYPOINT ["dotnet", "ProjectManagementSystem.Api.dll"]