using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Page object class that represents the Calendar home page
    /// and provides validations related to the initial page load state.
    /// </summary>
    public class CalendarHomePage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarHomePage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        public CalendarHomePage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
        {
        }

        /// <summary>
        /// Verifies whether the Calendar home page is loaded successfully.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the page title is not null or empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLoaded()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }
    }
}