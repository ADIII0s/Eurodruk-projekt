namespace Eurodruk.App.Data;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    public bool ApplyMigrationsOnStartup { get; set; } = true;
    public bool SeedOnStartup { get; set; } = false;
}
