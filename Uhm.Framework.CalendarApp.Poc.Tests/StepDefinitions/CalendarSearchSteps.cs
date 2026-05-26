using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for calendar search functionality.
    /// </summary>
    [Binding]
    public class CalendarSearchSteps
    {
        private readonly CalendarHomePage _calendarHomePage;
        private readonly TestData _testData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarSearchSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        public CalendarSearchSteps(IWebDriver driver, TestSettings settings)
        {
            _calendarHomePage = new CalendarHomePage(driver, settings.ExplicitWaitSeconds);
            _testData = TestData.Load();
        }

        /// <summary>
        /// Selects a team from the calendar search dropdown.
        /// </summary>
        [When(@"I select a team for calendar search")]
        public void WhenISelectATeamForCalendarSearch()
        {
            _calendarHomePage.SelectTeam(_testData.Calendar.DefaultTeam);
        }

        /// <summary>
        /// Clicks the Search button on the Calendar page.
        /// </summary>
        [When(@"I click the Search button")]
        public void WhenIClickTheSearchButton()
        {
            _calendarHomePage.ClickSearch();
        }

        /// <summary>
        /// Verifies that the selected team's calendar is displayed successfully.
        /// </summary>
        [Then(@"the team calendar should be displayed")]
        public void ThenTheTeamCalendarShouldBeDisplayed()
        {
            Assert.That(
                _calendarHomePage.IsCalendarDisplayed(),
                Is.True,
                "Team calendar was not displayed after search.");
        }
    }
}
