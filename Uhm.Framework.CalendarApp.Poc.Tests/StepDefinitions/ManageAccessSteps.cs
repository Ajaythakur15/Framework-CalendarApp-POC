using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions
{
    /// <summary>
    /// Step definition class that contains BDD step implementations
    /// for scenarios related to the Manage Access module.
    /// </summary>
    [Binding]
    public class ManageAccessSteps
    {
        private readonly ManageAccessPage _manageAccessPage;
        private readonly ScenarioContext _scenarioContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageAccessSteps"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="settings">The runtime test settings loaded from configuration.</param>
        /// <param name="scenarioContext">The current SpecFlow scenario context.</param>
        public ManageAccessSteps(
            IWebDriver driver,
            TestSettings settings,
            ScenarioContext scenarioContext)
        {
            _manageAccessPage = new ManageAccessPage(
                driver,
                settings.ExplicitWaitSeconds);

            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Opens the Manage Access module from the application navigation.
        /// </summary>
        [When(@"I open the Manage Access module")]
        public void WhenIOpenTheManageAccessModule()
        {
            _manageAccessPage.OpenManageAccess();
        }

        /// <summary>
        /// Clicks the Add Access button to launch the Add Access popup.
        /// </summary>
        [When(@"I click the Add Access button")]
        public void WhenIClickTheAddAccessButton()
        {
            Assert.That(
                _manageAccessPage.IsAccessPopupClosed(),
                Is.True,
                "Add Access popup is already displayed before clicking Add Access button.");

            _manageAccessPage.ClickAddAccess();
        }

        /// <summary>
        /// Verifies that the Add Access popup is displayed successfully.
        /// </summary>
        [Then(@"the Add Access popup should be displayed")]
        public void ThenTheAddAccessPopupShouldBeDisplayed()
        {
            Assert.That(
                _manageAccessPage.IsAddAccessPopupDisplayed(),
                Is.True,
                "Add Access popup was not displayed.");
        }

        /// <summary>
        /// Enters generated access details into the Add Access popup form.
        /// </summary>
        [When(@"I enter access details")]
        public void WhenIEnterAccessDetails()
        {
            //var uniqueEmail = $"bdd.user.{DateTime.Now:yyyyMMddHHmmss}@test.com";
            var uniqueEmail = $"asingh@uhm.com";

            _scenarioContext["CreatedAccessEmail"] = uniqueEmail;

            _manageAccessPage.SelectTeam("ServicingSupport");
            _manageAccessPage.EnterAccessDetails(uniqueEmail);
        }

        /// <summary>
        /// Saves the access record from the Add Access popup.
        /// </summary>
        [When(@"I save the access")]
        public void WhenISaveTheAccess()
        {
            _manageAccessPage.ClickSave();
        }

        /// <summary>
        /// Verifies that the Add Access popup closes after the save action.
        /// </summary>
        [Then(@"the access popup should close")]
        public void ThenTheAccessPopupShouldClose()
        {
            Assert.That(
                _manageAccessPage.IsAccessPopupClosed(),
                Is.True,
                "Add Access popup did not close after save.");
        }

        /// <summary>
        /// Deletes an existing access record from the Manage Access grid.
        /// </summary>
        [When(@"I delete an existing access record")]
        public void WhenIDeleteAnExistingAccessRecord()
        {
            var emailToDelete = _manageAccessPage.GetLastAccessRecordEmail();
            var countBeforeDelete = _manageAccessPage.GetAccessRecordCountByEmail(emailToDelete);

            _scenarioContext["DeletedAccessEmail"] = emailToDelete;
            _scenarioContext["DeletedAccessCountBefore"] = countBeforeDelete;

            _manageAccessPage.DeleteLastAccessRecord();
        }

        /// <summary>
        /// Verifies that the selected access record was removed successfully.
        /// </summary>
        [Then(@"the access record should be removed successfully")]
        public void ThenTheAccessRecordShouldBeRemovedSuccessfully()
        {
            var deletedEmail = _scenarioContext["DeletedAccessEmail"]?.ToString();
            var countBeforeDelete = Convert.ToInt32(_scenarioContext["DeletedAccessCountBefore"]);

            Assert.That(
                deletedEmail,
                Is.Not.Null.And.Not.Empty,
                "Deleted access email was not captured before delete.");

            Assert.That(
                _manageAccessPage.IsAccessRecordCountReducedByOne(deletedEmail!, countBeforeDelete),
                Is.True,
                $"Access record count for email '{deletedEmail}' was not reduced by one.");
        }
    }
}