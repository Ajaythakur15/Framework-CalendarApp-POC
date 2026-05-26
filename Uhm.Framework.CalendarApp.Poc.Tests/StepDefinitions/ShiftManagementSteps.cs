using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for Shift Management functionality in the Calendar module.
    /// </summary>
    [Binding]
    public class ShiftManagementSteps
    {
        private readonly CalendarHomePage _calendarHomePage;
        private readonly ScenarioContext _scenarioContext;
        private readonly TestData _testData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShiftManagementSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        /// <param name="scenarioContext">The current SpecFlow scenario context.</param>
        public ShiftManagementSteps(
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
        /// Opens the Shift Management tab in the Calendar module.
        /// </summary>
        [When(@"I open the Shift Management tab")]
        public void WhenIOpenTheShiftManagementTab()
        {
            _calendarHomePage.OpenShiftManagementTab();
        }

        /// <summary>
        /// Verifies that the Shift Management view is displayed.
        /// </summary>
        [Then(@"the Shift Management view should be displayed")]
        public void ThenTheShiftManagementViewShouldBeDisplayed()
        {
            Assert.That(
                _calendarHomePage.IsShiftManagementDisplayed(),
                Is.True,
                "Shift Management view was not displayed.");
        }

        /// <summary>
        /// Clicks the Add Shift button.
        /// </summary>
        [When(@"I click the Add Shift button")]
        public void WhenIClickTheAddShiftButton()
        {
            _calendarHomePage.ClickAddShift();
        }

        /// <summary>
        /// Verifies that the Add Shift popup is displayed.
        /// </summary>
        [Then(@"the Add Shift popup should be displayed")]
        public void ThenTheAddShiftPopupShouldBeDisplayed()
        {
            Assert.That(
                _calendarHomePage.IsAddShiftPopupDisplayed(),
                Is.True,
                "Add Shift popup was not displayed.");
        }

        /// <summary>
        /// Enters valid shift override details into the Add Shift popup.
        /// </summary>
        [When(@"I enter shift override details")]
        public void WhenIEnterShiftOverrideDetails()
        {
            var shiftTitle = $"Shift{DateTime.Now:HHmmss}";
            _scenarioContext["CreatedShiftTitle"] = shiftTitle;

            var startDate = DateTime.Now
                .AddDays(10)
                .Date
                .AddHours(16);

            var endDate = startDate.AddHours(2);

            _calendarHomePage.EnterShiftOverrideDetails(
                shiftTitle,
                startDate.ToString("MM/dd/yyyy h:mm tt"),
                endDate.ToString("MM/dd/yyyy h:mm tt"),
                _testData.Calendar.ShiftRemark);
        }

        /// <summary>
        /// Saves the shift override.
        /// </summary>
        [When(@"I save the shift override")]
        public void WhenISaveTheShiftOverride()
        {
            _calendarHomePage.ClickSaveShiftOverride();
        }

        /// <summary>
        /// Verifies that the Add Shift popup closes successfully.
        /// </summary>
        [Then(@"the Add Shift popup should close")]
        public void ThenTheAddShiftPopupShouldClose()
        {
            var errorMessage = _calendarHomePage.GetShiftValidationMessage();

            Assert.That(
                _calendarHomePage.IsAddShiftPopupClosed(),
                Is.True,
                string.IsNullOrWhiteSpace(errorMessage)
                    ? "Add Shift popup did not close successfully."
                    : $"Add Shift popup did not close successfully. Validation/Error: {errorMessage}");
        }
    }
}
