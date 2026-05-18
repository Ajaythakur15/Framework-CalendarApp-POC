using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(IWebDriver driver, int waitSeconds)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
    }

    public string Title => Driver.Title;
}