using AventStack.ExtentReports;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Uhm.Framework.CalendarApp.Poc.Tests.Drivers;
using Uhm.Framework.CalendarApp.Poc.Tests.Support;
using Uhm.Framework.CalendarApp.Poc.Tests.Utilities;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Hooks
{
    /// <summary>
    /// SpecFlow hooks class responsible for test setup and cleanup activities
    /// before and after each scenario execution.
    /// </summary>
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver? _driver;
        private ExtentTest? _scenario;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHooks"/> class
        /// with the required SpecFlow container and scenario context.
        /// </summary>
        /// <param name="container">The dependency injection container used by SpecFlow.</param>
        /// <param name="scenarioContext">The current scenario execution context.</param>
        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        /// <summary>
        /// Performs browser and test configuration setup before each scenario starts.
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            var settings = TestSettings.Load();
            _driver = WebDriverFactory.Create(settings.Headless);

            _container.RegisterInstanceAs(_driver);
            _container.RegisterInstanceAs(settings);

            _scenario = ExtentReportManager
                .GetInstance()
                .CreateTest(_scenarioContext.ScenarioInfo.Title);
        }

        /// <summary>
        /// Captures the result of each executed step and writes it into the Extent report.
        /// </summary>
        [AfterStep]
        public void AfterStep()
        {
            if (_scenario == null)
            {
                return;
            }

            var stepText = $"{_scenarioContext.StepContext.StepInfo.StepDefinitionType} {_scenarioContext.StepContext.StepInfo.Text}";

            if (_scenarioContext.TestError == null)
            {
                _scenario.Pass(stepText);
            }
            else
            {
                _scenario.Fail($"{stepText}<br>{_scenarioContext.TestError.Message}");
            }
        }

        /// <summary>
        /// Performs cleanup after each scenario, including screenshot capture on failure,
        /// browser shutdown, and report flushing.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (_scenarioContext.TestError != null && _driver != null && _scenario != null)
                {
                    var screenshotPath = ScreenshotHelper.TakeScreenshot(_driver, _scenarioContext.ScenarioInfo.Title);
                    _scenario.AddScreenCaptureFromPath(screenshotPath);
                    Console.WriteLine($"Screenshot saved: {screenshotPath}");
                }
            }
            finally
            {
                _driver?.Quit();
                ExtentReportManager.Flush();
            }
        }
    }
}