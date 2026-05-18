using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions;

[Binding]
public class ManageAccessSteps
{
    private readonly ManageAccessPage _manageAccessPage;
    private readonly ScenarioContext _scenarioContext;

    public ManageAccessSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
    {
        _manageAccessPage = new ManageAccessPage(driver, settings.ExplicitWaitSeconds);
        _scenarioContext = scenarioContext;
    }

    [When(@"I open the Manage Access module")]
    public void WhenIOpenTheManageAccessModule()
    {
        _manageAccessPage.OpenManageAccess();
    }

    [When(@"I click the Add Access button")]
    public void WhenIClickTheAddAccessButton()
    {
        _manageAccessPage.ClickAddAccess();
    }

    [When(@"I enter access details")]
    public void WhenIEnterAccessDetails()
    {
        var uniqueEmail = $"bdd.user.{DateTime.Now:yyyyMMddHHmmss}@example.com";
        _scenarioContext["CreatedAccessEmail"] = uniqueEmail;

        _manageAccessPage.SelectTeam();
        _manageAccessPage.EnterAccessDetails("ManagerAccess", uniqueEmail);
    }

    [When(@"I save the access")]
    public void WhenISaveTheAccess()
    {
        _manageAccessPage.ClickSave();
    }

    [Then(@"the Add Access popup should be displayed")]
    public void ThenTheAddAccessPopupShouldBeDisplayed()
    {
        Assert.That(_manageAccessPage.IsAddAccessPopupDisplayed(), Is.True);
    }

    [Then(@"the access popup should close")]
    public void ThenTheAccessPopupShouldClose()
    {
        Assert.That(_manageAccessPage.IsAccessPopupClosed(), Is.True);
    }
}
