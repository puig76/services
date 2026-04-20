using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

namespace E2E.Tests
{
    public class BookingFlowTests
    {
        [Fact]
        public async Task LandingPageHasBookingButton()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://localhost:5000/");

            var title = await page.TitleAsync();
            Assert.NotEmpty(title);

            var hasBooking = (await page.QuerySelectorAsync("button#start-booking")) != null
                             || (await page.QuerySelectorAsync("text=Book")) != null
                             || (await page.QuerySelectorAsync("text=Booking")) != null;

            Assert.True(hasBooking, "Booking flow button should exist on the landing page");
        }
    }
}
