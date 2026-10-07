# PersonalAssetVault

A full-stack dashboard for tracking personal assets, with login. I built it as a hands-on exercise in Clean Architecture and modern Angular (Signals).

## Architecture
The core domain has no dependencies on frameworks or the database:
- **Domain:** entities, rules, and repository contracts
- **Application:** use cases, DTOs, interfaces, and mapping
- **Infrastructure:** EF Core, repositories, and the JWT token provider
- **API:** thin ASP.NET Core controllers, DI setup, and CORS/auth middleware
- **client-app:** Angular SPA

## Stack
**Backend:** .NET 9 · ASP.NET Core Web API · EF Core with SQLite (code-first, Fluent API) · Mapster · BCrypt · JWT bearer auth · ProblemDetails error handling · Scalar API docs

**Frontend:** Angular 18 (standalone) · Signals and RxJS for state · Tailwind CSS · functional interceptors and route guards · reactive forms

## Why SQLite?
So anyone can clone and run it with no database server or Docker. Because the database sits behind EF Core and repositories, moving to PostgreSQL means switching `UseSqlite()` to `UseNpgsql()` and generating a new migration. More decisions are in [DECISIONS_LOG.md](DECISIONS_LOG.md).

## Run it locally
Prerequisites: .NET 9 SDK, Node.js 20+, Angular CLI.
1. Create the database:
   `dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project API/API.csproj`
2. Run the API: `cd API && dotnet run`
   The API starts at `https://localhost:7123`, and Scalar docs are at `/scalar/v1` in development.
3. Run the client: `cd client-app && npm install && ng serve`
4. Open `http://localhost:4200`. You'll be redirected to the login screen.

## What's next
- Unit tests for the Application layer
- Asset history and simple charts
- Refresh tokens

---

Built by Jovan Madzic, Software Engineer in Belgrade · [LinkedIn](https://www.linkedin.com/in/jovan-madzic-12093b202/) · [GitHub](https://github.com/ckejoM)
