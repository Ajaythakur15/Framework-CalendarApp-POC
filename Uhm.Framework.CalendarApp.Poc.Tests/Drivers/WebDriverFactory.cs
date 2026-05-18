using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Drivers;

public static class WebDriverFactory
{
    public static IWebDriver Create(bool headless = true)
    {
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        return new ChromeDriver(options);
    }
}