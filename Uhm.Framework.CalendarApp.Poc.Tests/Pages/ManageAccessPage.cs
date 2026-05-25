using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Page object class that contains UI actions and validations
    /// for the Manage Access module of the Calendar application.
    /// </summary>
    public class ManageAccessPage : BasePage
    {
        private readonly By _manageAccessTab = By.XPath("//a[contains(@href,'calendar/manage-access') or contains(.,'MANAGE ACCESS')]");
        private readonly By _addAccessButton = By.XPath("//button[contains(.,'ADD ACCESS')]");
        private readonly By _addAccessPopupHeader = By.XPath("//*[contains(text(),'Add Access')]");
        private readonly By _teamDropdown = By.XPath("//select");
        private readonly By _emailInput = By.XPath("//label[contains(.,'Email')]/following::input[1]");
        private readonly By _saveButton = By.XPath("//button[contains(.,'Save')]");
        private readonly By _popupHeader = By.XPath("//*[contains(text(),'Add Access')]");

        private readonly By _lastRowEmail =
            By.XPath("(//div[contains(@class,'access-body-wrapper')])[last()]//div[contains(@class,'field-three')]");

        private readonly By _lastRowDeleteButton =
            By.XPath("(//div[contains(@class,'access-body-wrapper')])[last()]//div[contains(@class,'field-four')]/*[local-name()='svg' and ./*[local-name()='polyline']]");

        private readonly By _confirmDeleteButton =
            By.XPath("//button[normalize-space()='DELETE']");

        private readonly By _confirmDeletePopupHeader =
            By.XPath("//*[contains(text(),'Confirm Delete')]");

        private readonly By _teamMembersDropdown =
            By.XPath("//label[contains(.,'Team')]/following::select[2]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageAccessPage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        public ManageAccessPage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
        {
        }

        /// <summary>
        /// Opens the Manage Access module by clicking the corresponding navigation tab.
        /// </summary>
        public void OpenManageAccess()
        {
            var manageAccessTab = Wait.Until(d =>
            {
                var element = d.FindElement(_manageAccessTab);

                return element.Displayed && element.Enabled
                    ? element
                    : null;
            });

            if (manageAccessTab == null)
            {
                throw new NoSuchElementException("Manage Access tab was not found.");
            }

            manageAccessTab.Click();

            Wait.Until(d => d.Url.Contains("manage-access"));
        }

        /// <summary>
        /// Clicks the Add Access button to open the access creation popup.
        /// </summary>
        public void ClickAddAccess()
        {
            Wait.Until(d => d.FindElement(_addAccessButton)).Click();
        }

        /// <summary>
        /// Verifies whether the Add Access popup is displayed.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the Add Access popup is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddAccessPopupDisplayed()
        {
            return Wait.Until(d => d.FindElement(_addAccessPopupHeader)).Displayed;
        }

        /// <summary>
        /// Selects a team from the available team dropdown.
        /// </summary>
        public void SelectTeam(string teamName)
        {
            var dropdownElement = Wait.Until(d => d.FindElement(_teamDropdown));
            var dropdown = new SelectElement(dropdownElement);
            dropdown.SelectByText(teamName);
        }

        /// <summary>
        /// Gets the available Team Members values from the team members dropdown.
        /// </summary>
        /// <returns>
        /// A collection of team members option texts.
        /// </returns>
        public IReadOnlyCollection<string> GetAvailableTeamMembers()
        {
            var options = Wait.Until(d =>
                d.FindElements(By.XPath("//label[contains(.,'Team')]/following::select[1]/option")));

            return options
                .Select(o => o.Text.Trim())
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .ToList();
        }

        /// <summary>
        /// Selects a team member from the dropdown.
        /// </summary>
        public void SelectTeamMembers(string teamName)
        {
            var dropdown = new SelectElement(
                Wait.Until(d => d.FindElement(_teamMembersDropdown)));

            dropdown.SelectByText(teamName);
        }

        /// <summary>
        /// Enters the required access details into the Add Access form.
        /// </summary>
        /// <param name="email">The email address to be entered.</param>
        public void EnterAccessDetails(string email)
        {
            var emailElement = WaitForElement(_emailInput);
            emailElement.Clear();
            emailElement.SendKeys(email);
        }

        /// <summary>
        /// Clicks the Save button on the Add Access popup.
        /// </summary>
        public void ClickSave()
        {
            Wait.Until(d => d.FindElement(_saveButton)).Click();
        }

        /// <summary>
        /// Verifies whether the Add Access popup is closed after save.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the popup is no longer visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAccessPopupClosed()
        {
            try
            {
                Wait.Until(_ => Driver.FindElements(_popupHeader).Count == 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the email address from the last access record row.
        /// </summary>
        /// <returns>
        /// The email value from the last visible access record.
        /// </returns>
        public string GetLastAccessRecordEmail()
        {
            return WaitForElement(_lastRowEmail).Text.Trim();
        }

        /// <summary>
        /// Gets the current number of access records matching the provided email.
        /// </summary>
        /// <param name="email">The email address to count in the grid.</param>
        /// <returns>
        /// The number of rows currently displaying the specified email.
        /// </returns>
        public int GetAccessRecordCountByEmail(string email)
        {
            return Driver.FindElements(
                By.XPath($"//div[contains(@class,'field-three') and normalize-space()='{email}']")).Count;
        }

        /// <summary>
        /// Deletes the last available access record from the Manage Access grid.
        /// </summary>
        public void DeleteLastAccessRecord()
        {
            var deleteIcon = WaitForElement(_lastRowDeleteButton);
            JsClick(deleteIcon);

            var confirmDeleteButton = WaitForElement(_confirmDeleteButton);
            JsClick(confirmDeleteButton);
        }

        /// <summary>
        /// Verifies whether the confirm delete popup is closed after delete.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the confirm delete popup is no longer visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAccessRecordRemovedSuccessfully()
        {
            try
            {
                Wait.Until(_ => Driver.FindElements(_confirmDeletePopupHeader).Count == 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verifies whether the number of matching access records for the given email
        /// is reduced by one after deletion.
        /// </summary>
        /// <param name="email">The email captured before deletion.</param>
        /// <param name="previousCount">The number of matching rows before delete.</param>
        /// <returns>
        /// <c>true</c> if the count is reduced by one; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAccessRecordCountReducedByOne(string email, int previousCount)
        {
            try
            {
                Wait.Until(_ => GetAccessRecordCountByEmail(email) == previousCount - 1);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}