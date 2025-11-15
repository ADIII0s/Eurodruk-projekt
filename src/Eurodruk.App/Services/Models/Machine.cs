namespace Eurodruk.App.Services.Models;

public class Machine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Line { get; set; } = string.Empty;
    public string Segment { get; set; } = string.Empty;
    public string? Department { get; set; }
}
