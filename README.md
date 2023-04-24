# ProjectManagementSystem.Api

The Project Management System API is a RESTful web interface created using ASP.NET Core, C#, DDD and CQRS. This API provides endpoints for managing projects and tasks, allowing users to create, read, update, and delete:

- Projects
- Issues
- Labels for issues
- Time entries for issues
- Comments for issues
- Reactions (emojis) for issues and comments

The API uses a PostgreSQL database to store data and utilizes JSON Web Tokens (JWT) for user authentication and authorization. Additionally, the project includes Swagger UI for API documentation and testing.

## Getting Started
To get started with the Project Management System API, simply clone the repository and run it on your local machine using either Visual Studio or .NET CLI. You will also need to set up a PostgreSQL database (version 15 and above) and update the appsettings.json file with your connection string.

Alternatively, you can run the API using [ProjectManagementSystem.Docker](https://github.com/gradovenko/ProjectManagementSystem.Docker)

## License
This project is not licensed ([NO LICENSE](NOLICENSE)). All rights to the project belong to the author of the project.
