using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Base page class that provides common Selenium page functionality
    /// for all page objects in the Calendar App automation framework.
    /// </summary>
    public abstract class BasePage
    {
        /// <summary>
        /// Gets the Selenium WebDriver instance used by the page object.
        /// </summary>
        protected readonly IWebDriver Driver;

        /// <summary>
        /// Gets the explicit wait instance used for element synchronization.
        /// </summary>
        protected readonly WebDriverWait Wait;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class
        /// with the shared driver and wait configuration.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        protected BasePage(IWebDriver driver, int waitSeconds)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
        }

        /// <summary>
        /// Gets the current browser page title.
        /// </summary>
        public string Title => Driver.Title;
    }
}