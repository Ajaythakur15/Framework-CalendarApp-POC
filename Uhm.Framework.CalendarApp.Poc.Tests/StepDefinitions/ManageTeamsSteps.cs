using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions;

[Binding]
public class ManageTeamsSteps
{
    private readonly ManageTeamsPage _manageTeamsPage;
    private readonly ScenarioContext _scenarioContext;

    public ManageTeamsSteps(IWebDriver driver, TestSettings settings, ScenarioContext scenarioContext)
    {
        _manageTeamsPage = new ManageTeamsPage(driver, settings.ExplicitWaitSeconds);
        _scenarioContext = scenarioContext;
    }

    [When(@"I open the Manage Teams module")]
    public void WhenIOpenTheManageTeamsModule()
    {
        _manageTeamsPage.OpenManageTeams();
    }

    [When(@"I click the Add Team button")]
    public void WhenIClickTheAddTeamButton()
    {
        _manageTeamsPage.ClickAddTeam();
    }

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

    [When(@"I save the team")]
    public void WhenISaveTheTeam()
    {
        _manageTeamsPage.ClickSave();
    }

    [Then(@"the Add Team popup should be displayed")]
    public void ThenTheAddTeamPopupShouldBeDisplayed()
    {
        Assert.That(_manageTeamsPage.IsAddTeamPopupDisplayed(), Is.True);
    }

    [Then(@"the team popup should close")]
    public void ThenTheTeamPopupShouldClose()
    {
        Assert.That(_manageTeamsPage.IsAddTeamPopupClosed(), Is.True);
    }
}