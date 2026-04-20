# Playwright E2E Testing

This repository includes Playwright E2E test support for .NET using `Microsoft.Playwright`.

## E2E Tests

### Project Location
- **Tests**: `tests/E2E/E2E.csproj`
- **Test Class**: `SampleE2ETests.cs` (example tests against localhost:5000)

### Local Setup & Execution

#### Prerequisites
- .NET 10 SDK
- Playwright CLI and browser binaries

#### Install Playwright
```bash
dotnet tool install --tool-path .dotnet/tools Microsoft.Playwright.CLI
.dotnet/tools/playwright install --with-deps
```

Or use global install:
```bash
dotnet tool install -g Microsoft.Playwright.CLI
playwright install --with-deps
```

#### Build & Run Tests Locally

1. **Start the web app** (in one terminal):
   ```bash
   dotnet run --project src/WebApp/WebApp.csproj
   # App will run on http://localhost:5000
   ```

2. **Run all E2E tests** (in another terminal):
   ```bash
   dotnet test ./tests/E2E/E2E.csproj --configuration Release
   ```

3. **Run specific E2E test by name**:
   ```bash
   dotnet test ./tests/E2E/E2E.csproj --filter "FullyQualifiedName~ShouldNavigateToLocalhost"
   ```

### CI/CD

The GitHub Actions workflow (`.github/workflows/playwright.yml`) automatically:
- Sets up .NET 10
- Installs Playwright CLI and browser dependencies
- Builds the solution
- Starts the web app (localhost:5000)
- Runs E2E tests
- Cleans up resources

The workflow runs on push to `main`/`develop` and on pull requests targeting those branches.

### Adding New Tests

1. Create a new test file in `tests/E2E/` (e.g., `MyFeatureTests.cs`)
2. Use xUnit and Playwright APIs:
   ```csharp
   using Microsoft.Playwright;
   using Xunit;

   public class MyFeatureTests : IAsyncLifetime
   {
       private IPage? _page;
       
       public async Task InitializeAsync() 
       { 
           // Setup browser & page
       }
       
       public async Task DisposeAsync() 
       { 
           // Cleanup
       }
       
       [Fact]
       public async Task MyTest() 
       { 
           // Write test
       }
   }
   ```

3. Run tests locally to verify before committing.

### Troubleshooting

- **"Browser not found"**: Run `playwright install --with-deps` again.
- **"Connection refused on localhost:5000"**: Ensure the web app is running in another terminal.
- **Playwright timeout issues**: Adjust timeouts in test code (e.g., `page.GotoAsync(..., new PageGotoOptions { Timeout = 30000 })`).
