using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace E2E.Tests
{
    public class BookingFlowTests
    {
        [Test]
        public async Task LandingPageHasBookingButton()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("http://localhost:5000/");

            Assert.That(await page.TitleAsync(), Is.Not.Empty, "Landing page title should not be empty");

            var hasBooking = (await page.QuerySelectorAsync("button#start-booking")) != null
                             || (await page.QuerySelectorAsync("text=Book")) != null
                             || (await page.QuerySelectorAsync("text=Booking")) != null;

            Assert.IsTrue(hasBooking, "Booking flow button should exist on the landing page");
        }
    }
}
