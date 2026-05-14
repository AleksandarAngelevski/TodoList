# TodoList

A task management web application built with Blazor and PostgreSQL.

## Features

- Add and manage tasks
- User authentication and registration
- Audit tracking (created by, modified by, timestamps)
- Persistent storage with PostgreSQL

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Blazor (ASP.NET Core) |
| Backend | .NET 10 |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Auth | ASP.NET Core Identity |

## Architecture

The project follows **Onion Architecture** with the following layers:

- `TodoList.Domain` — Entities, interfaces, base classes
- `TodoList.Repository` — EF Core DbContext, migrations, repositories
- `TodoList.Service` — Business logic, service interfaces
- `TodoList.Web` — Blazor UI, controllers, interceptors

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (local or [Neon](https://neon.tech) free tier)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/TodoList.git
   cd TodoList
   ```

2. Update the connection string in `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=TodoListDb;Username=postgres;"
     }
   }
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update --project TodoList.Repository --startup-project TodoList.Web
   ```

4. Run the app:
   ```bash
   dotnet run --project TodoList.Web
   ```
