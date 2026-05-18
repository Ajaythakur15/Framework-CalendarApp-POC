using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for validating the Calendar application smoke scenario.
    /// </summary>
    [Binding]
    public class CalendarSmokeSteps
    {
        private readonly IWebDriver _driver;
        private readonly TestSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarSmokeSteps"/> class
        /// with the WebDriver and framework settings for the current scenario.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance for the current test run.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        public CalendarSmokeSteps(IWebDriver driver, TestSettings settings)
        {
            _driver = driver;
            _settings = settings;
        }

        /// <summary>
        /// Navigates the browser to the configured Calendar application URL.
        /// </summary>
        [Given(@"I navigate to the calendar application")]
        public void GivenINavigateToTheCalendarApplication()
        {
            _driver.Navigate().GoToUrl(_settings.AppUrl);
        }

        /// <summary>
        /// Validates that the Calendar home page is displayed successfully.
        /// </summary>
        [Then(@"the calendar home page should be displayed")]
        public void ThenTheCalendarHomePageShouldBeDisplayed()
        {
            var homePage = new CalendarHomePage(_driver, _settings.ExplicitWaitSeconds);
            Assert.That(homePage.IsLoaded(), Is.True);
        }
    }
}