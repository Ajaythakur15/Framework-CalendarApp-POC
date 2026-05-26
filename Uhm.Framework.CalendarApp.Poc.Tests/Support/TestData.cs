using System.Text.Json;

namespace Uhm.Framework.CalendarApp.Poc.Tests.Support
{
    /// <summary>
    /// Represents reusable test input data used by the Calendar App BDD automation framework.
    /// </summary>
    public class TestData
    {
        public CalendarTestData Calendar { get; set; } = new();
        public ManageAccessTestData ManageAccess { get; set; } = new();
        public ManageTeamsTestData ManageTeams { get; set; } = new();

        /// <summary>
        /// Loads test data from the testdata.json file located in the output directory.
        /// </summary>
        /// <returns>
        /// A populated <see cref="TestData"/> object containing reusable test input values.
        /// </returns>
        public static TestData Load()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "testdata.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Test data file not found: {filePath}");
            }

            var json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<TestData>(json);

            if (data == null)
            {
                throw new InvalidOperationException("Failed to deserialize testdata.json.");
            }

            return data;
        }
    }

    public class CalendarTestData
    {
        public string DefaultTeam { get; set; } = string.Empty;
        public string EventCategory { get; set; } = string.Empty;
        public string EventRemark { get; set; } = string.Empty;
        public string ShiftRemark { get; set; } = string.Empty;
    }

    public class ManageAccessTestData
    {
        public string DefaultTeam { get; set; } = string.Empty;
        public string DefaultEmailDomain { get; set; } = string.Empty;
    }

    public class ManageTeamsTestData
    {
        public string ReferenceTeam { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
