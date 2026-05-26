using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages
{
    /// <summary>
    /// Page object class that contains UI actions and validations
    /// for the Manage Teams module of the Calendar application.
    /// </summary>
    public class ManageTeamsPage : BasePage
    {
        private readonly By _manageTeamsTab = By.XPath("//a[contains(@href,'manage-team') or contains(.,'MANAGE TEAMS')]");
        private readonly By _addTeamButton = By.XPath("//button[contains(.,'ADD TEAM')]");
        private readonly By _addTeamPopupHeader = By.XPath("//*[contains(text(),'Add Team')]");
        private readonly By _teamTitleInput = By.XPath("//label[contains(.,'TEAM TITLE')]/following::input[1]");
        private readonly By _descriptionInput = By.XPath("//label[contains(.,'DESCRIPTION')]/following::textarea[1]");
        private readonly By _referenceTeamDropdown = By.XPath("//label[contains(.,'REFERENCE TEAM')]/following::*[contains(@class,'select') or self::select][1]");
        private readonly By _saveButton = By.XPath("//button[contains(.,'SAVE')]");
        private readonly By _closePopupHeader = By.XPath("//*[contains(text(),'Add Team')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageTeamsPage"/> class.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="waitSeconds">The explicit wait timeout in seconds.</param>
        public ManageTeamsPage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
        {
        }

        /// <summary>
        /// Opens the Manage Teams module by clicking the corresponding navigation tab.
        /// </summary>
        public void OpenManageTeams()
        {
            Wait.Until(d => d.FindElement(_manageTeamsTab)).Click();
        }

        /// <summary>
        /// Clicks the Add Team button to open the team creation popup.
        /// </summary>
        public void ClickAddTeam()
        {
            Wait.Until(d => d.FindElement(_addTeamButton)).Click();
        }

        /// <summary>
        /// Verifies whether the Add Team popup is displayed.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the Add Team popup is visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddTeamPopupDisplayed()
        {
            return Wait.Until(d => d.FindElement(_addTeamPopupHeader)).Displayed;
        }

        /// <summary>
        /// Enters the required team details into the Add Team form.
        /// </summary>
        /// <param name="teamTitle">The team title to be entered.</param>
        /// <param name="description">The description to be entered.</param>
        public void EnterTeamDetails(string teamTitle, string description)
        {
            var teamTitleElement = Wait.Until(d => d.FindElement(_teamTitleInput));
            teamTitleElement.Clear();
            teamTitleElement.SendKeys(teamTitle);

            var descriptionElement = Wait.Until(d => d.FindElement(_descriptionInput));
            descriptionElement.Clear();
            descriptionElement.SendKeys(description);
        }

        /// <summary>
        /// Selects a reference team from the available dropdown options.
        /// </summary>
        public void SelectReferenceTeam(string teamName)
        {
            var dropdown = Wait.Until(d => d.FindElement(_referenceTeamDropdown));
            dropdown.Click();

            var option = Wait.Until(d =>
                d.FindElement(By.XPath($"//*[contains(@class,'option') or @role='option' or self::option][contains(.,'{teamName}')][1]")));
            option.Click();
        }

        /// <summary>
        /// Clicks the Save button on the Add Team popup.
        /// </summary>
        public void ClickSave()
        {
            Wait.Until(d => d.FindElement(_saveButton)).Click();
        }

        /// <summary>
        /// Verifies whether the Add Team popup is closed after save.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the popup is no longer visible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddTeamPopupClosed()
        {
            try
            {
                Wait.Until(_ => Driver.FindElements(_closePopupHeader).Count == 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
