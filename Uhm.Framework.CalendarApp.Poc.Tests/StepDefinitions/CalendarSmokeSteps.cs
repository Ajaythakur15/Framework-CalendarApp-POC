using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Pages;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.StepDefinitions;

[Binding]
public class CalendarSmokeSteps
{
    private readonly IWebDriver _driver;
    private readonly TestSettings _settings;

    public CalendarSmokeSteps(IWebDriver driver, TestSettings settings)
    {
        _driver = driver;
        _settings = settings;
    }

    [Given(@"I navigate to the calendar application")]
    public void GivenINavigateToTheCalendarApplication()
    {
        _driver.Navigate().GoToUrl(_settings.AppUrl);
    }

    [Then(@"the calendar home page should be displayed")]
    public void ThenTheCalendarHomePageShouldBeDisplayed()
    {
        var homePage = new CalendarHomePage(_driver, _settings.ExplicitWaitSeconds);
        Assert.That(homePage.IsLoaded(), Is.True);
    }
}
