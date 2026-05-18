using OpenQA.Selenium;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Utilities
{
    /// <summary>
    /// Helper class responsible for capturing and saving browser screenshots
    /// during test execution, typically on failure.
    /// </summary>
    public static class ScreenshotHelper
    {
        /// <summary>
        /// Captures a screenshot of the current browser window and saves it
        /// into the latest test results screenshot folder.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver instance.</param>
        /// <param name="scenarioTitle">The title of the current test scenario.</param>
        /// <returns>
        /// The full file path of the saved screenshot.
        /// </returns>
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
}