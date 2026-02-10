# Anime Collection

REST API for managing a personal anime collection with filtering, sorting, pagination, and image uploads.

## Tech Stack

- **Backend:** ASP.NET Core 8.0 / C#
- **Database:** PostgreSQL via Entity Framework Core 9.0
- **Frontend:** React (localhost:5173, separate project)
- **Docs:** Swagger / OpenAPI
- **Containerization:** Docker

## Architecture

The solution follows a layered (clean) architecture with four projects:

```
Anime_Collection.sln
├── Anime_Collection   # Web API — controllers, DTOs, startup
├── BusinessLogic      # Service layer
├── Core               # Domain models, interfaces
└── DataAccess         # EF Core DbContext, repositories, migrations
```

## Project Structure

```
Anime_Collection/
├── Controllers/
│   ├── AnimeController.cs        # CRUD + filtering endpoints
│   └── FileUploadController.cs   # Image upload endpoint
├── Contracts/
│   ├── Request.cs                # Create/update DTO
│   ├── Response.cs               # Response DTO
│   ├── SearchParams.cs           # Filter/sort/pagination params
│   └── PaginResponse.cs          # Paginated response wrapper
├── wwwroot/images/               # Uploaded images
├── Program.cs                    # App configuration & DI
├── Dockerfile
└── docker-compose.yml

BusinessLogic/
└── Services/Service.cs           # Business logic implementation

Core/
├── Models/Anime.cs               # Domain model with validation
└── Interfaces/
    ├── IService.cs
    └── IRepository.cs

DataAccess/
├── AppDbContext.cs                # EF Core context
├── Entities/Entity.cs            # Database entity
├── Repository/Repository.cs      # Data access implementation
├── Configurations/Configuration.cs
└── Migrations/
```

## Domain Model

**Anime**

| Field  | Type     | Constraints                              |
|--------|----------|------------------------------------------|
| Id     | UUID     | Primary key                              |
| Title  | string   | Required, max 100 chars                  |
| Rating | double   | 0–10                                     |
| Status | enum     | Planned, Watching, Completed, Dropped    |
| Genres | string[] | At least one, each max 35 chars          |
| Image  | string?  | Optional path to uploaded image          |

## API Endpoints

### Anime

| Method | Route            | Description                                |
|--------|------------------|--------------------------------------------|
| GET    | `/anime`         | Get all anime                              |
| GET    | `/anime/filter`  | Search with filtering, sorting, pagination |
| POST   | `/anime`         | Create a new anime entry                   |
| PUT    | `/anime/{id}`    | Update an existing entry                   |
| DELETE | `/anime/{id}`    | Delete an entry                            |

**Filter query parameters** (`/anime/filter`):

| Param      | Type   | Default | Description                               |
|------------|--------|---------|-------------------------------------------|
| `search`   | string | —       | Search query                              |
| `sortBy`   | string | —       | Field to filter/sort: `title`, `genre`, `status` |
| `sortDesc` | bool   | false   | Sort descending                           |
| `page`     | int    | 1       | Page number                               |
| `count`    | int    | 10      | Items per page                            |

### File Upload

| Method | Route                 | Description          |
|--------|-----------------------|----------------------|
| POST   | `/fileupload/upload`  | Upload an anime image |

Allowed formats: PNG, JPG, JPEG, GIF. Max size: 5 MB.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/) (or Docker)

### Database Setup

Start PostgreSQL via Docker Compose:

```bash
cd Anime_Collection
docker-compose up -d
```

Or ensure a local PostgreSQL instance is running and update the connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "AppDbContext": "Host=localhost;Port=5432;Database=Ayaya;Username=postgres;Password=1111"
  }
}
```

Apply migrations:

```bash
dotnet ef database update --project DataAccess --startup-project Anime_Collection
```

### Run

```bash
dotnet run --project Anime_Collection
```

Swagger UI will be available at `https://localhost:<port>/swagger` in Development mode.
