using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for creating a new event in the Calendar module.
    /// </summary>
    [Binding]
    public class CreateEventSteps
    {
        private readonly CalendarHomePage _calendarHomePage;
        private readonly ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        /// <param name="scenarioContext">The current SpecFlow scenario context.</param>
        public CreateEventSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
        {
            _calendarHomePage = new CalendarHomePage(driver, settings.ExplicitWaitSeconds);
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Enters event details into the Add Event popup form.
        /// </summary>
        [When(@"I enter event details")]
        public void WhenIEnterEventDetails()
        {
            var eventTitle = $"BDD Event {DateTime.Now:yyyyMMddHHmmss}";
            _scenarioContext["CreatedEventTitle"] = eventTitle;

            _calendarHomePage.SelectCategory("Holiday");
            _calendarHomePage.EnterEventDetails(
                eventTitle,
                "05/20/2026 10:00 AM",
                "05/20/2026 11:00 AM",
                "Created through Calendar App BDD POC");
        }

        /// <summary>
        /// Saves the event from the Add Event popup.
        /// </summary>
        [When(@"I save the event")]
        public void WhenISaveTheEvent()
        {
            _calendarHomePage.ClickSaveEvent();
        }

        /// <summary>
        /// Verifies that the Add Event popup closes after saving the event.
        /// </summary>
        [Then(@"the Add Event popup should close")]
        public void ThenTheAddEventPopupShouldClose()
        {
            Assert.That(_calendarHomePage.IsAddEventPopupClosed(), Is.True);
        }
    }
}