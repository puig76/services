# Copilot Instructions — services

This guide helps Copilot and team members work effectively on the services project. The project is a .NET-based system with a Blazor frontend and backend services architecture, coordinated by an AI agent squad.

## Quick Start: Build, Test, and Lint

### Build

```bash
# Build all projects (when projects exist)
dotnet build

# Build a specific project
dotnet build src/YourProject/YourProject.csproj
```

### Run Tests

```bash
# Run all tests
dotnet test

# Run a single test file
dotnet test --filter "ClassName"

# Run a specific test method
dotnet test --filter "ClassName.MethodName"

# Run tests with detailed output
dotnet test --verbosity detailed
```

### Lint and Format

```bash
# Format code (using .NET formatting)
dotnet format

# Check formatting without changes
dotnet format --verify-no-changes
```

> **Status:** This repository structure is being initialized. Actual `.csproj` files and projects will be added by the backend (Fenster) and frontend (Dallas) developers. Check `src/` and adjust commands above once projects are present.

## High-Level Architecture

This is a distributed services system composed of several tiers:

- **Blazor Frontend** (Dallas) — Interactive web UI built with Blazor Server or Blazor WebAssembly, located in `src/Web/` or similar. Handles client-side routing, components, and user interactions.
- **Backend Services** (Fenster) — RESTful or gRPC APIs built with ASP.NET Core, implementing domain logic, validation, and integrations. APIs are the contracts between frontend and backend.
- **Persistence Layer** — SQL Server, PostgreSQL, or similar. Managed by Entity Framework Core migrations and data access patterns.
- **Integration Points** — External systems, queues (RabbitMQ, Azure Service Bus), or event streams. Handled by the backend services layer.
- **DevOps & CI/CD** (Redfoot) — GitHub Actions workflows in `.github/workflows/`, deployment configurations, and infrastructure as code.

Entry points to trace:
- **Frontend**: Look in `src/Web/Program.cs` (Blazor Server) or `src/Web.Client/Program.cs` (WASM) for DI setup and routing configuration.
- **Backend**: Look in `src/Api/Program.cs` (or service entry point) for middleware, route registration, and service configuration.
- **Data**: Look in `src/Data/` (or `src/Infrastructure/`) for DbContext, migrations, and repository patterns.

Cross-file concepts require reading:
- API contracts between frontend and backend (check controller routes and Blazor service calls)
- Domain models (shared contracts or separate frontend/backend models, depending on architecture)
- Configuration and dependency injection setup in `Program.cs` files

## Key Conventions

### Squad Team Coordination

This project uses an AI agent squad for parallel work:

| Role | Handles | Agent Name |
|------|---------|-----------|
| **Lead** (Architecture) | System design, API contracts, domain models, integrations | **Keaton** |
| **Frontend Dev** (Blazor) | UI components, client routing, accessibility | **Dallas** |
| **Backend Dev** (.NET) | APIs, services, persistence, integration tests | **Fenster** |
| **Tester / QA** | E2E tests, test plans, quality gates | **Hockney** |
| **DevOps** | CI/CD pipelines, deployment, infrastructure as code | **Redfoot** |

**For issue routing:** Issues labeled with `squad` are triaged by the Lead (Keaton). Once assigned a specific `squad:{member}` label (e.g., `squad:fenster` for backend work), that member picks up the work.

### Git Workflow

- **Default branch:** `dev` (integration branch for all features)
- **Release branch:** `main` (tagged releases only)
- **Branch naming:** `squad/{issue-number}-{kebab-case-slug}`
  - Example: `squad/42-add-payment-api`
- **Commit message trailer:** All commits must include:
  ```
  Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
  ```

Always branch from `dev`, not `main`. Submit PRs back to `dev` unless doing a release.

### Folder Structure (Expected)

Once initialized, the typical structure is:

```
src/
  ├── Web/                    # Blazor Frontend (Dallas)
  │   ├── Program.cs
  │   ├── Components/         # Blazor components
  │   └── Services/           # API clients
  ├── Api/                    # Backend API (Fenster)
  │   ├── Program.cs
  │   ├── Controllers/
  │   ├── Services/
  │   └── Models/
  ├── Data/                   # Persistence (Fenster)
  │   ├── DbContext.cs
  │   └── Migrations/
  └── Tests/                  # Integration/E2E tests (Hockney)
      ├── Api.Tests/
      └── Web.Tests/
.github/
  ├── workflows/              # CI/CD pipelines (Redfoot)
  └── copilot-instructions.md # This file
.squad/
  ├── team.md                 # Squad roster
  ├── decisions.md            # Architecture decisions
  └── agents/                 # Per-agent notes
```

### Naming and Style

- **C# Classes & Methods:** PascalCase (`PaymentService`, `ProcessPayment()`)
- **Properties:** PascalCase (`UserId`, `CreatedAt`)
- **Private fields:** camelCase with underscore prefix (`_logger`, `_repository`)
- **Constants:** PascalCase (`DefaultTimeout`, `ApiVersion`)
- **Blazor Components:** PascalCase, `.razor` extension (`LoginForm.razor`, `Dashboard.razor`)

### Communication & Decisions

- **Architecture decisions:** Documented in `.squad/decisions.md` (requires team consensus)
- **Per-agent knowledge:** Stored in `.squad/agents/{name}/history.md`
- **Issues & PRs:** Use GitHub labels for routing (`squad`, `squad:member-name`, `type:bug`, `type:feature`)

### Testing Expectations

- **Unit tests:** In-project test classes or `{ProjectName}.Tests` projects. Use xUnit or NUnit.
- **Integration tests:** In `src/Tests/` or `{Service}.Tests/` for API integration with the database.
- **E2E tests:** Browser-based tests (Selenium, Playwright, or similar) in `src/Tests/` folder, handled by Hockney.

Run single tests with `dotnet test --filter "Namespace.ClassName.MethodName"`.

### CI/CD & Deployment

- **Workflows:** In `.github/workflows/` (managed by Redfoot)
- **Status checks:** Linting, build, and tests must pass before merge
- **Deployment:** Triggered by merge to `dev` (preview channel) or tags on `main` (production)

---

**Last updated:** April 2026  
**Team:** Keaton (Lead), Dallas (Frontend), Fenster (Backend), Hockney (QA), Redfoot (DevOps)
