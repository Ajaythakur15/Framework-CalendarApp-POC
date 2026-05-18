using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Utilities;

public static class ScreenshotHelper
{
    public static string TakeScreenshot(IWebDriver driver, string scenarioTitle)
    {
        var reportRoot = Path.Combine(AppContext.BaseDirectory, "TestResults");
        var latestRunFolder = Directory.GetDirectories(reportRoot)
            .OrderByDescending(Directory.GetCreationTime)
            .FirstOrDefault();

        if (latestRunFolder == null)
        {
            latestRunFolder = Path.Combine(reportRoot, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(latestRunFolder);
        }

        var screenshotsDirectory = Path.Combine(latestRunFolder, "Screenshots");
        Directory.CreateDirectory(screenshotsDirectory);

        var safeScenarioTitle = string.Concat(scenarioTitle.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{safeScenarioTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(screenshotsDirectory, fileName);

        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(filePath);

        return filePath;
    }
}
