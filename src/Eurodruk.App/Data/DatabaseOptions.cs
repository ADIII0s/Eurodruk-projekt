using Microsoft.Extensions.Configuration;

namespace Eurodruk.App.Data;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    public bool ApplyMigrationsOnStartup { get; set; } = true;
    public bool SeedOnStartup { get; set; } = false;
}

public static class DatabaseOptionsExtensions
{
    public static DatabaseOptions GetDatabaseOptions(this IConfiguration configuration)
    {
        return configuration.Get<DatabaseOptions>() ?? new DatabaseOptions();
    }
}
