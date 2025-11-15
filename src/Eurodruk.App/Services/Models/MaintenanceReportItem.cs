namespace Eurodruk.App.Services.Models;

public class MaintenanceReportItem
{
    public int Id { get; set; }
    public int ReportId { get; set; }
    public MaintenanceReport? Report { get; set; }
    public int TicketId { get; set; }
    public WorkshopTicket? Ticket { get; set; }
    public string Summary { get; set; } = string.Empty;
}
