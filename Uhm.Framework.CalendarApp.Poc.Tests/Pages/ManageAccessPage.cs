using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Page object class that contains UI actions and validations
    /// for the Manage Access module of the Calendar application.
    /// </summary>
    public class ManageAccessPage : BasePage
    {
        private readonly By _manageAccessTab = By.XPath("//a[contains(@href,'manage-access') or contains(.,'MANAGE ACCESS')]");
        private readonly By _addAccessButton = By.XPath("//button[contains(.,'ADD ACCESS')]");
        private readonly By _addAccessPopupHeader = By.XPath("//*[contains(text(),'Add Access')]");
        private readonly By _teamDropdown = By.XPath("//label[contains(.,'TEAM')]/following::*[contains(@class,'select') or self::select][1]");
        private readonly By _roleInput = By.XPath("//label[contains(.,'ROLE')]/following::input[1]");
        private readonly By _emailInput = By.XPath("//label[contains(.,'EMAIL')]/following::input[1]");
        private readonly By _saveButton = By.XPath("//button[contains(.,'SAVE')]");
        private readonly By _popupHeader = By.XPath("//*[contains(text(),'Add Access')]");

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
            Wait.Until(d => d.FindElement(_manageAccessTab)).Click();
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
        public void SelectTeam()
        {
            var dropdown = Wait.Until(d => d.FindElement(_teamDropdown));
            dropdown.Click();

            var option = Wait.Until(d =>
                d.FindElement(By.XPath("//*[contains(@class,'option') or @role='option' or self::option][contains(.,'Union Home Mortgage') or contains(.,'ServicingSupport')][1]")));
            option.Click();
        }

        /// <summary>
        /// Enters the required access details into the Add Access form.
        /// </summary>
        /// <param name="role">The role to be entered.</param>
        /// <param name="email">The email address to be entered.</param>
        public void EnterAccessDetails(string role, string email)
        {
            var roleElement = Wait.Until(d => d.FindElement(_roleInput));
            roleElement.Clear();
            roleElement.SendKeys(role);

            var emailElement = Wait.Until(d => d.FindElement(_emailInput));
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
    }
}