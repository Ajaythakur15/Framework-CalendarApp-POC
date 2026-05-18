using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Drivers
{
    /// <summary>
    /// Factory class used to create and return configured Selenium WebDriver instances
    /// for browser-based test execution in the Calendar App BDD automation framework.
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Creates a Chrome WebDriver instance with predefined browser options
        /// for local and automated test execution.
        /// </summary>
        /// <param name="headless">
        /// Indicates whether the browser should run in headless mode.
        /// By default, headless execution is enabled.
        /// </param>
        /// <returns>
        /// An <see cref="IWebDriver"/> instance configured for Chrome browser execution.
        /// </returns>
        public static IWebDriver Create(bool headless = true)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless=new");
            }

            options.AddArgument("--start-maximized");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            return new ChromeDriver(options);
        }
    }
}