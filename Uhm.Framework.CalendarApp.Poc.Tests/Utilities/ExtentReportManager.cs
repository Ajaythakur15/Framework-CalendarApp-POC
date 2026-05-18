using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Utilities;

public static class ExtentReportManager
{
    private static ExtentReports? _extent;
    private static readonly object LockObject = new();

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

    public static void Flush()
    {
        _extent?.Flush();
    }
}
