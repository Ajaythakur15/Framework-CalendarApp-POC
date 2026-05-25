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

            Wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(waitSeconds));
        }

        /// <summary>
        /// Gets the current browser page title.
        /// </summary>
        public string Title => Driver.Title;

        /// <summary>
        /// Waits until the specified element is visible and enabled,
        /// then returns the web element.
        /// </summary>
        /// <param name="locator">
        /// The Selenium locator used to find the element.
        /// </param>
        /// <returns>
        /// The visible and enabled web element.
        /// </returns>
        protected IWebElement WaitForElement(By locator)
        {
            return Wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);

                    return element.Displayed && element.Enabled
                        ? element
                        : null;
                }
                catch
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// Clicks an element using JavaScript executor.
        /// Useful for dynamic, overlayed, or React-based UI elements
        /// where standard Selenium click actions may fail.
        /// </summary>
        /// <param name="element">
        /// The target web element to be clicked.
        /// </param>
        protected void JsClick(IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch
            {
                ((IJavaScriptExecutor)Driver)
                    .ExecuteScript(
                        "arguments[0].dispatchEvent(new MouseEvent('click', { bubbles: true }));",
                        element);
            }
        }

        /// <summary>
        /// Gets text from an element safely.
        /// </summary>
        /// <param name="locator">
        /// The Selenium locator.
        /// </param>
        /// <returns>
        /// The element text if found; otherwise empty string.
        /// </returns>
        protected string GetElementText(By locator)
        {
            try
            {
                return WaitForElement(locator).Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Verifies whether an element is displayed.
        /// </summary>
        /// <param name="locator">
        /// The Selenium locator.
        /// </param>
        /// <returns>
        /// <c>true</c> if displayed; otherwise <c>false</c>.
        /// </returns>
        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return WaitForElement(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}