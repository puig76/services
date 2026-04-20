using Microsoft.Playwright;
using Xunit;

namespace E2E.Tests
{
    public class SampleE2ETests : IAsyncLifetime
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private IPage? _page;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync();
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        public async Task DisposeAsync()
        {
            await _page?.CloseAsync()!;
            await _context?.CloseAsync()!;
            await _browser?.CloseAsync()!;
            _playwright?.Dispose();
        }

        [Fact]
        public async Task ShouldNavigateToLocalhost()
        {
            // Example: Verify the web app responds on localhost:5000
            var response = await _page!.GotoAsync("http://localhost:5000/");
            Assert.NotNull(response);
            Assert.True(response.Ok || response.Status < 400, $"Expected successful response, got {response.Status}");
        }

        [Fact]
        public async Task ShouldHavePageTitle()
        {
            // Example: Check page title or heading
            await _page!.GotoAsync("http://localhost:5000/");
            var title = await _page.TitleAsync();
            Assert.False(string.IsNullOrEmpty(title), "Page should have a title");
        }
    }
}
