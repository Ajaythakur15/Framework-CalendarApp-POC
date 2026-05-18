using System.Text.Json;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Support;

public class TestSettings
{
    public string AppUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = "chrome";
    public bool Headless { get; set; } = true;
    public int ExplicitWaitSeconds { get; set; } = 20;

    public static TestSettings Load()
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TestSettings>(json)
               ?? throw new InvalidOperationException("Failed to load appsettings.json");
    }
}