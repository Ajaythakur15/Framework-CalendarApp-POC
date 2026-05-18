using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Drivers;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Hooks;

[Binding]
public class TestHooks
{
    private readonly IObjectContainer _container;
    private IWebDriver? _driver;

    public TestHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var settings = TestSettings.Load();
        _driver = WebDriverFactory.Create(settings.Headless);
        _container.RegisterInstanceAs(_driver);
        _container.RegisterInstanceAs(settings);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driver?.Quit();
    }
}