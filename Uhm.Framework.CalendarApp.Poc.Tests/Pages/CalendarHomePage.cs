using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Pages;

public class CalendarHomePage : BasePage
{
    public CalendarHomePage(IWebDriver driver, int waitSeconds) : base(driver, waitSeconds)
    {
    }

    public bool IsLoaded()
    {
        return !string.IsNullOrWhiteSpace(Title);
    }
}