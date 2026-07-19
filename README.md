# MangaFlex

MangaFlex is an ASP.NET Core MVC manga reader that uses MangaDex for manga discovery, cover art, metadata, and chapter pages. It includes user registration/login with ASP.NET Core Identity and keeps a small "last read" history for signed-in users.

## Features

- Browse safe, English-translated manga from MangaDex.
- Search manga by title.
- View manga details, including cover, description, year, language, volume/chapter metadata, and status.
- Read manga chapters in the browser with previous/next chapter navigation.
- Register, log in, log out, and view a user profile.
- Track recently opened manga for authenticated users.

## Tech Stack

- ASP.NET Core MVC
- Razor views
- Entity Framework Core
- PostgreSQL through `Npgsql.EntityFrameworkCore.PostgreSQL`
- ASP.NET Core Identity
- MediatR
- MangaDexSharp
- xUnit, Moq, and coverlet for tests

## Solution Structure

```text
MangaFlex/
+-- MangaFlex.sln
+-- src/
|   +-- MangaFlex.Core/            # Domain models, commands, handlers, service contracts
|   +-- MangaFlex.Infrastructure/  # EF Core DbContext and external service implementations
|   +-- MangaFlex.Presentation/    # ASP.NET Core MVC app, controllers, views, migrations
+-- XUNIT_MangaFlex/               # xUnit test project
```

## Requirements

- .NET SDK with support for the app projects targeting `net7.0`.
- .NET SDK support for the test project targeting `net8.0`.
- PostgreSQL database.
- Internet access at runtime so the app can call MangaDex.
- Optional: `dotnet-ef` for running EF Core migrations from the command line.

Install the EF Core CLI tool if you do not already have it:

```powershell
dotnet tool install --global dotnet-ef
```

## Configuration

The web app expects a PostgreSQL connection string named `MangaFlex`.

For local development, prefer user secrets so database credentials do not live in source control:

```powershell
dotnet user-secrets init --project src/MangaFlex.Presentation
dotnet user-secrets set "ConnectionStrings:MangaFlex" "Host=localhost;Port=5432;Database=mangaflex;Username=postgres;Password=<your-password>" --project src/MangaFlex.Presentation
```

You can also provide the connection string through an environment variable:

```powershell
$env:ConnectionStrings__MangaFlex="Host=localhost;Port=5432;Database=mangaflex;Username=postgres;Password=<your-password>"
```

## Database Setup

Create a PostgreSQL database, then apply the existing EF Core migrations:

```powershell
dotnet ef database update --project src/MangaFlex.Presentation --startup-project src/MangaFlex.Presentation
```

The current migration creates the Identity tables plus the `UserLastWatch` table used for recently read manga.

## Run the App

Restore dependencies:

```powershell
dotnet restore MangaFlex.sln
```

Run the MVC app:

```powershell
dotnet run --project src/MangaFlex.Presentation --launch-profile https
```

Open one of the configured local URLs:

- `https://localhost:7098`
- `http://localhost:5150`

## Useful Routes

- `/` - home page.
- `/Mangas` - browse manga.
- `/Mangas?search=one%20piece` - search manga by title.
- `/Manga/About?id=<manga-id>` - manga details page. Requires login.
- `/Manga/Read?id=<manga-id>&chapter=1` - chapter reader.
- `/User/Registration` - create an account.
- `/User/Login` - sign in.
- `/User/Profile` - profile and recent reads. Requires login.

## Run Tests

The test project is kept in its own solution:

```powershell
dotnet test XUNIT_MangaFlex/XUNIT_MangaFlex.sln
```

## Development Notes

- Manga data is not stored locally. `MangaService` fetches catalog results, metadata, cover URLs, and chapter pages through MangaDexSharp.
- The app filters MangaDex results to safe content with available English chapters.
- ASP.NET Identity persists users and authentication data in PostgreSQL.
- `MangaController.About` updates the signed-in user's recent-read list when a manga details page is opened.
- The root solution currently includes the app projects. The xUnit project lives under `XUNIT_MangaFlex`.

## License

No license file is currently included.
