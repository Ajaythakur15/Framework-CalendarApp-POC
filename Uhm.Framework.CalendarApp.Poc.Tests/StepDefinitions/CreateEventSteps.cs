using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for creating calendar events.
    /// </summary>
    [Binding]
    public class CreateEventSteps
    {
        private readonly CalendarHomePage _calendarHomePage;
        private readonly ScenarioContext _scenarioContext;
        private readonly TestData _testData;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        /// <param name="scenarioContext">The current SpecFlow scenario context.</param>
        public CreateEventSteps(
            IWebDriver driver,
            TestSettings settings,
            ScenarioContext scenarioContext)
        {
            _calendarHomePage = new CalendarHomePage(
                driver,
                settings.ExplicitWaitSeconds);

            _scenarioContext = scenarioContext;
            _testData = TestData.Load();
        }

        /// <summary>
        /// Enters valid event details into the Add Event popup.
        /// </summary>
        [When(@"I enter valid event details")]
        public void WhenIEnterValidEventDetails()
        {
            var eventTitle = $"Automation Event {DateTime.Now:yyyyMMddHHmmss}";
            _scenarioContext["CreatedEventTitle"] = eventTitle;

            var startDate = DateTime.Now
                .AddDays(10)
                .Date
                .AddHours(15)
                .AddMinutes(DateTime.Now.Second % 30);

            var endDate = startDate.AddHours(1);

            _calendarHomePage.SelectCategory(_testData.Calendar.EventCategory);

            _calendarHomePage.EnterEventDetails(
                eventTitle,
                startDate.ToString("MM/dd/yyyy h:mm tt"),
                endDate.ToString("MM/dd/yyyy h:mm tt"),
                _testData.Calendar.EventRemark);
        }

        /// <summary>
        /// Enters overlapping event details into the Add Event popup.
        /// </summary>
        [When(@"I enter overlapping event details")]
        public void WhenIEnterOverlappingEventDetails()
        {
            var eventTitle = $"Overlap Event {DateTime.Now:yyyyMMddHHmmss}";

            var startDate = DateTime.Today
                .AddDays(1)
                .AddHours(9);

            var endDate = startDate.AddHours(1);

            const string overlapRemark = "Overlap validation test";

            _calendarHomePage.SelectCategory(_testData.Calendar.EventCategory);

            _calendarHomePage.EnterEventDetails(
                eventTitle,
                startDate.ToString("MM/dd/yyyy h:mm tt"),
                endDate.ToString("MM/dd/yyyy h:mm tt"),
                overlapRemark);
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
        /// Verifies that the overlap validation message is displayed.
        /// </summary>
        [Then(@"the overlap validation message should be displayed")]
        public void ThenTheOverlapValidationMessageShouldBeDisplayed()
        {
            var validationMessage = _calendarHomePage.GetEventValidationMessage();

            Assert.That(
                validationMessage,
                Does.Contain("Time slot unavailable"),
                "Expected overlap validation message was not displayed.");
        }

        /// <summary>
        /// Verifies that the Add Event popup remains open.
        /// </summary>
        [Then(@"the Add Event popup should remain open")]
        public void ThenTheAddEventPopupShouldRemainOpen()
        {
            Assert.That(
                _calendarHomePage.IsAddEventPopupDisplayed(),
                Is.True,
                "Add Event popup was unexpectedly closed.");
        }

        /// <summary>
        /// Verifies that the Add Event popup closes successfully.
        /// </summary>
        [Then(@"the Add Event popup should close")]
        public void ThenTheAddEventPopupShouldClose()
        {
            var errorMessage = _calendarHomePage.GetEventSaveErrorMessage();

            Assert.That(
                _calendarHomePage.IsAddEventPopupClosed(),
                Is.True,
                string.IsNullOrWhiteSpace(errorMessage)
                    ? "Add Event popup did not close successfully."
                    : $"Add Event popup did not close successfully. Validation/Error: {errorMessage}");
        }
    }
}
