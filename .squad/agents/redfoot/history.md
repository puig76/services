# History — Redfoot
## Learnings
- Seeded with project context.
- **Playwright CI Workflow Validation (2025-04-20):** Workflow correctly specifies .NET 10 setup and installs Playwright CLI via `dotnet tool install`. Browser installation via `playwright install --with-deps` requires sudo for system deps (apt packages), but `playwright install` (sans deps) succeeds and downloads browsers to ~/.cache/ms-playwright/. Workflow works as designed; local validation confirmed Playwright binaries download successfully. E2E tests properly fail when localhost:5000 is unavailable (expected behavior in CI when web app not started).
