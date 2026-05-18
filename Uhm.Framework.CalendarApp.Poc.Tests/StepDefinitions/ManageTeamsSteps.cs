using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for scenarios related to the Manage Teams module.
    /// </summary>
    [Binding]
    public class ManageTeamsSteps
    {
        private readonly ManageTeamsPage _manageTeamsPage;
        private readonly ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageTeamsSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        /// <param name="scenarioContext">The current SpecFlow scenario context.</param>
        public ManageTeamsSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
        {
            _manageTeamsPage = new ManageTeamsPage(driver, settings.ExplicitWaitSeconds);
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Opens the Manage Teams module from the application navigation.
        /// </summary>
        [When(@"I open the Manage Teams module")]
        public void WhenIOpenTheManageTeamsModule()
        {
            _manageTeamsPage.OpenManageTeams();
        }

        /// <summary>
        /// Clicks the Add Team button to launch the Add Team popup.
        /// </summary>
        [When(@"I click the Add Team button")]
        public void WhenIClickTheAddTeamButton()
        {
            _manageTeamsPage.ClickAddTeam();
        }

        /// <summary>
        /// Enters generated team details into the Add Team popup form.
        /// </summary>
        [When(@"I enter team details")]
        public void WhenIEnterTeamDetails()
        {
            var uniqueTeamName = $"BDD Team {DateTime.Now:yyyyMMddHHmmss}";
            _scenarioContext["CreatedTeamName"] = uniqueTeamName;

            _manageTeamsPage.EnterTeamDetails(
                uniqueTeamName,
                "Created through BDD Calendar App POC");
            _manageTeamsPage.SelectReferenceTeam();
        }

        /// <summary>
        /// Saves the team record from the Add Team popup.
        /// </summary>
        [When(@"I save the team")]
        public void WhenISaveTheTeam()
        {
            _manageTeamsPage.ClickSave();
        }

        /// <summary>
        /// Verifies that the Add Team popup is displayed successfully.
        /// </summary>
        [Then(@"the Add Team popup should be displayed")]
        public void ThenTheAddTeamPopupShouldBeDisplayed()
        {
            Assert.That(_manageTeamsPage.IsAddTeamPopupDisplayed(), Is.True);
        }

        /// <summary>
        /// Verifies that the Add Team popup closes after the save action.
        /// </summary>
        [Then(@"the team popup should close")]
        public void ThenTheTeamPopupShouldClose()
        {
            Assert.That(_manageTeamsPage.IsAddTeamPopupClosed(), Is.True);
        }
    }
}