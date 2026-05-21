using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for opening the Add Event popup in the Calendar module.
    /// </summary>
    [Binding]
    public class AddEventSteps
    {
        private readonly CalendarHomePage _calendarHomePage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddEventSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        public AddEventSteps(IWebDriver driver, TestSettings settings)
        {
            _calendarHomePage = new CalendarHomePage(driver, settings.ExplicitWaitSeconds);
        }

        /// <summary>
        /// Clicks the Add Event button on the Calendar page.
        /// </summary>
        [When(@"I click the Add Event button")]
        public void WhenIClickTheAddEventButton()
        {
            _calendarHomePage.ClickAddEvent();
        }

        /// <summary>
        /// Verifies that the Add Event popup is displayed successfully.
        /// </summary>
        [Then(@"the Add Event popup should be displayed")]
        public void ThenTheAddEventPopupShouldBeDisplayed()
        {
            Assert.That(_calendarHomePage.IsAddEventPopupDisplayed(), Is.True);
        }
    }
}