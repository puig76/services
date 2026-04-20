Playwright E2E tests

Prereqs: Ensure Playwright browsers are installed locally:
  dotnet tool install --global Microsoft.Playwright.CLI || true
  playwright install chromium

Run a single test locally:
  dotnet test ./tests/E2E/E2E.csproj --filter FullyQualifiedName~BookingFlowTests

Tests run headless by default for CI.
