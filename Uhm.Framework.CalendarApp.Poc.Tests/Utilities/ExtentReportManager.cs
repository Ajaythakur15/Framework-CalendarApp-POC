using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Utilities
{
    /// <summary>
    /// Manager class responsible for creating, configuring, and flushing
    /// the Extent HTML report used during test execution.
    /// </summary>
    public static class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static readonly object LockObject = new();

        /// <summary>
        /// Returns the singleton instance of the Extent report object,
        /// creating and configuring it if it does not already exist.
        /// </summary>
        /// <returns>
        /// A configured <see cref="ExtentReports"/> instance.
        /// </returns>
        public static ExtentReports GetInstance()
        {
            lock (LockObject)
            {
                if (_extent == null)
                {
                    var reportDirectory = Path.Combine(AppContext.BaseDirectory, "TestResults", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    Directory.CreateDirectory(reportDirectory);
                    Directory.CreateDirectory(Path.Combine(reportDirectory, "Screenshots"));

                    var reportPath = Path.Combine(reportDirectory, "ExtentReport.html");
                    var htmlReporter = new ExtentSparkReporter(reportPath);

                    _extent = new ExtentReports();
                    _extent.AttachReporter(htmlReporter);
                }

                return _extent;
            }
        }

        /// <summary>
        /// Flushes the current Extent report instance and writes the report output to disk.
        /// </summary>
        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}