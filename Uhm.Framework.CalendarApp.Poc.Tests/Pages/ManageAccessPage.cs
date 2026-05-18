using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages;

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

    public ManageAccessPage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
    {
    }

    public void OpenManageAccess()
    {
        Wait.Until(d => d.FindElement(_manageAccessTab)).Click();
    }

    public void ClickAddAccess()
    {
        Wait.Until(d => d.FindElement(_addAccessButton)).Click();
    }

    public bool IsAddAccessPopupDisplayed()
    {
        return Wait.Until(d => d.FindElement(_addAccessPopupHeader)).Displayed;
    }

    public void SelectTeam()
    {
        var dropdown = Wait.Until(d => d.FindElement(_teamDropdown));
        dropdown.Click();

        var option = Wait.Until(d =>
            d.FindElement(By.XPath("//*[contains(@class,'option') or @role='option' or self::option][contains(.,'Union Home Mortgage') or contains(.,'ServicingSupport')][1]")));
        option.Click();
    }

    public void EnterAccessDetails(string role, string email)
    {
        var roleElement = Wait.Until(d => d.FindElement(_roleInput));
        roleElement.Clear();
        roleElement.SendKeys(role);

        var emailElement = Wait.Until(d => d.FindElement(_emailInput));
        emailElement.Clear();
        emailElement.SendKeys(email);
    }

    public void ClickSave()
    {
        Wait.Until(d => d.FindElement(_saveButton)).Click();
    }

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
