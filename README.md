# DomumBackend - ASP.NET Core Clean Architecture with CQRS and Identity

This project is a .NET 8.0 implementation of Clean Architecture, CQRS pattern, and Identity for role and user management. It includes features for managing young persons, facilities, rooms, and user access control. The backend utilizes SQL Server, Dapper, Entity Framework, AutoMapper, MediatR, and JWT for authentication and authorization in an ASP.NET Core Web API.

## Technologies Used
- ASP.NET Core 8.0
- C#
- Clean Architecture
- CQRS Pattern
- Identity (Role and User Management)
- SQL Server
- Dapper
- Entity Framework Core
- AutoMapper
- MediatR
- JWT Authentication and Authorization
- Swagger/OpenAPI

## Project Structure
```
DomumBackend/
├── DomumBackend.Api/              # ASP.NET Core Web API
├── DomumBackend.Application/       # Application layer (CQRS Commands/Queries)
├── DomumBackend.Domain/            # Domain layer (Entities)
├── DomumBackend.Infrastructure/    # Infrastructure layer (Data, Services, Identity)
└── DomumBackend.UnitTests/         # Unit tests
```

## How to Run the Project

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (local or remote)
- Visual Studio 2022 (optional, but recommended)

### Setup Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/domumbackend.git
   cd domumbackend
   ```

2. Open the solution file `DomumBackend.sln` in Visual Studio or use the CLI.

3. Configure your SQL Server connection in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=DomumBackend;Trusted_Connection=true;"
     }
   }
   ```

4. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

5. Run database migrations:
   ```bash
   dotnet ef database update --project DomumBackend.Infrastructure
   ```

6. Build the project:
   ```bash
   dotnet build
   ```

7. Run the API:
   ```bash
   dotnet run --project DomumBackend.Api
   ```

The API will be available at `https://localhost:5001` (or the configured port).

## API Documentation
Swagger/OpenAPI documentation is available at `/swagger` when the application is running.

## Key Features
- **User Management**: Create, read, update, delete users with role-based access control
- **Role Management**: Manage user roles and permissions
- **Young Person Management**: Track and manage young persons associated with facilities
- **Facility Management**: Manage facilities with associated rooms and user access
- **Room Management**: Manage rooms within facilities
- **JWT Authentication**: Secure API endpoints with JWT tokens
- **CQRS Pattern**: Separate read and write operations for scalability

## Additional Notes
- Always secure sensitive information such as connection strings and secret keys in `appsettings.Production.json`
- Configure environment-specific settings in separate appsettings files
- Use the provided HTTP file (`Naxxum.JobyHunter.Authentication.Api.http`) for testing endpoints
- Review and customize DTOs, Commands, and Queries according to your requirements

## License
This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing
Feel free to contribute by submitting a pull request or opening an issue for bugs and feature requests!

