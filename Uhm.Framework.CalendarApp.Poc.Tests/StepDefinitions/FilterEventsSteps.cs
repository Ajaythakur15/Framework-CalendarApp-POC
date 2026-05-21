using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for filtering calendar events by category.
    /// </summary>
    [Binding]
    public class FilterEventsSteps
    {
        private readonly CalendarHomePage _calendarHomePage;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterEventsSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        public FilterEventsSteps(IWebDriver driver, TestSettings settings)
        {
            _calendarHomePage = new CalendarHomePage(driver, settings.ExplicitWaitSeconds);
        }

        /// <summary>
        /// Selects an event category from the calendar filter control.
        /// </summary>
        [When(@"I select an event filter category")]
        public void WhenISelectAnEventFilterCategory()
        {
            _calendarHomePage.SelectFilterCategory("Holiday");
        }

        /// <summary>
        /// Verifies that filtered calendar content is displayed successfully.
        /// </summary>
        [Then(@"filtered calendar events should be displayed")]
        public void ThenFilteredCalendarEventsShouldBeDisplayed()
        {
            Assert.That(_calendarHomePage.IsFilteredCalendarDisplayed(), Is.True);
        }
    }
}