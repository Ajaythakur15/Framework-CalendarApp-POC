using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for validating available event categories in the Add Event popup.
    /// </summary>
    [Binding]
    public class EventCategorySteps
    {
        private readonly CalendarHomePage _calendarHomePage;
        private readonly TestData _testData;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventCategorySteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        public EventCategorySteps(IWebDriver driver, TestSettings settings)
        {
            _calendarHomePage = new CalendarHomePage(driver, settings.ExplicitWaitSeconds);
            _testData = TestData.Load();
        }

        /// <summary>
        /// Opens the event category dropdown inside the Add Event popup.
        /// </summary>
        [When(@"I open the event category dropdown")]
        public void WhenIOpenTheEventCategoryDropdown()
        {
            _calendarHomePage.OpenCategoryDropdown();
        }

        /// <summary>
        /// Verifies that the expected event categories are displayed.
        /// </summary>
        [Then(@"the available event categories should be displayed")]
        public void ThenTheAvailableEventCategoriesShouldBeDisplayed()
        {
            var categories = _calendarHomePage.GetAvailableCategories();

            Assert.That(categories, Does.Contain(_testData.Calendar.EventCategory));
            Assert.That(categories, Does.Contain("Holiday"));
            Assert.That(categories, Does.Contain("Bill's Bonus Day"));
        }
    }
}
