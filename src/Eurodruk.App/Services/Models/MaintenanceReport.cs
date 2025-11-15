namespace Eurodruk.App.Services.Models;

public class MaintenanceReport
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<MaintenanceReportItem> Items { get; set; } = new List<MaintenanceReportItem>();
}
