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
        private readonly TestData _testData;

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
            _testData = TestData.Load();
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
            var uniqueEmail =
                $"{_testData.ManageAccess.DefaultEmailPrefix}{DateTime.Now:HHmmss}@{_testData.ManageAccess.DefaultEmailDomain}";

            _scenarioContext["CreatedAccessEmail"] = uniqueEmail;

            _manageAccessPage.SelectTeam(_testData.ManageAccess.DefaultTeam);
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
        /// Verifies that the selected access record count was reduced by one.
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

        /// <summary>
        /// Clicks edit on the last access record row.
        /// </summary>
        [When(@"I edit an existing access record")]
        public void WhenIEditAnExistingAccessRecord()
        {
            var currentEmail = _manageAccessPage.GetLastAccessRecordEmail();
            _scenarioContext["OriginalAccessEmail"] = currentEmail;

            _manageAccessPage.ClickEditLastAccessRecord();
        }

        /// <summary>
        /// Updates access details in the edit popup.
        /// </summary>
        [When(@"I update the access details")]
        public void WhenIUpdateTheAccessDetails()
        {
            var updatedEmail =
                $"edit{DateTime.Now:HHmmss}@{_testData.ManageAccess.DefaultEmailDomain}";
            _scenarioContext["UpdatedAccessEmail"] = updatedEmail;

            _manageAccessPage.UpdateAccessDetails(updatedEmail);
        }

        /// <summary>
        /// Saves the updated access record.
        /// </summary>
        [When(@"I save the updated access")]
        public void WhenISaveTheUpdatedAccess()
        {
            _manageAccessPage.ClickUpdateAccess();
        }

        /// <summary>
        /// Verifies that the updated access record was saved successfully.
        /// </summary>
        [Then(@"the access record should be updated successfully")]
        public void ThenTheAccessRecordShouldBeUpdatedSuccessfully()
        {
            var updatedEmail = _scenarioContext["UpdatedAccessEmail"]?.ToString();

            Assert.That(
                _manageAccessPage.IsEditAccessPopupClosed(),
                Is.True,
                "Edit Access popup did not close after update.");

            Assert.That(
                updatedEmail,
                Is.Not.Null.And.Not.Empty,
                "Updated access email was not captured.");

            Assert.That(
                _manageAccessPage.IsAccessRecordPresent(updatedEmail!),
                Is.True,
                $"Updated access record with email '{updatedEmail}' was not found in the grid.");
        }
    }
}
