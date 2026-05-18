using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages;

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

    public ManageTeamsPage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
    {
    }

    public void OpenManageTeams()
    {
        Wait.Until(d => d.FindElement(_manageTeamsTab)).Click();
    }

    public void ClickAddTeam()
    {
        Wait.Until(d => d.FindElement(_addTeamButton)).Click();
    }

    public bool IsAddTeamPopupDisplayed()
    {
        return Wait.Until(d => d.FindElement(_addTeamPopupHeader)).Displayed;
    }

    public void EnterTeamDetails(string teamTitle, string description)
    {
        var teamTitleElement = Wait.Until(d => d.FindElement(_teamTitleInput));
        teamTitleElement.Clear();
        teamTitleElement.SendKeys(teamTitle);

        var descriptionElement = Wait.Until(d => d.FindElement(_descriptionInput));
        descriptionElement.Clear();
        descriptionElement.SendKeys(description);
    }

    public void SelectReferenceTeam()
    {
        var dropdown = Wait.Until(d => d.FindElement(_referenceTeamDropdown));
        dropdown.Click();

        var option = Wait.Until(d =>
            d.FindElement(By.XPath("//*[contains(@class,'option') or @role='option' or self::option][contains(.,'Union Home Mortgage') or contains(.,'ServicingSupport')][1]")));
        option.Click();
    }

    public void ClickSave()
    {
        Wait.Until(d => d.FindElement(_saveButton)).Click();
    }

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