using System.Text.Json;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Support
{
    /// <summary>
    /// Represents runtime test configuration values used by the
    /// Calendar App BDD automation framework.
    /// </summary>
    public class TestSettings
    {
        /// <summary>
        /// Gets or sets the application URL to be used during test execution.
        /// </summary>
        public string AppUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the browser name to be used for test execution.
        /// </summary>
        public string Browser { get; set; } = "chrome";

        /// <summary>
        /// Gets or sets a value indicating whether the browser should run in headless mode.
        /// </summary>
        public bool Headless { get; set; } = true;

        /// <summary>
        /// Gets or sets the explicit wait timeout in seconds.
        /// </summary>
        public int ExplicitWaitSeconds { get; set; } = 20;

        /// <summary>
        /// Loads test settings from the appsettings.json file located in the output directory.
        /// </summary>
        /// <returns>
        /// A populated <see cref="TestSettings"/> object containing runtime configuration values.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the appsettings.json file cannot be found.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the configuration file cannot be deserialized.
        /// </exception>
        public static TestSettings Load()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Configuration file not found: {filePath}");
            }

            var json = File.ReadAllText(filePath);
            var settings = JsonSerializer.Deserialize<TestSettings>(json);

            if (settings == null)
            {
                throw new InvalidOperationException("Failed to deserialize appsettings.json.");
            }

            return settings;
        }
    }
}