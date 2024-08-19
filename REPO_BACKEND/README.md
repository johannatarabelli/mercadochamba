# Backend

This project is an ASP.NET Core application that uses Entity Framework Core to connect to a SQL Server database. 

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) (optional, but recommended for database management)

## Development server

Run `dotnet run` to start the development server. Navigate to https://localhost:7243 to view the application.

## Database Migrations

Run `dotnet ef database update` to apply any pending migrations to the database.

## Swagger

Swagger is configured to generate API documentation. It is enabled by default in the development environment. Navigate to https://localhost:7243/swagger to view the Swagger UI.

## Running unit tests

Run `dotnet test` to execute the unit tests.

## Further help

To get more help on ASP.NET Core, visit the [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0).

## Add env variables
To add environment variables, create a `.env` file in the root directory and run the application.
