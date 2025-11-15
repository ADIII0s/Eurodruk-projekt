namespace Eurodruk.App.Services.Models;

public class WorkshopTicket
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string ProductionLine { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string OperatorName { get; set; } = string.Empty;
    public string OperatorDescription { get; set; } = string.Empty;
    public string? ServiceDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public ICollection<TicketPhoto> Photos { get; set; } = new List<TicketPhoto>();
    public ICollection<TicketAction> Actions { get; set; } = new List<TicketAction>();
    public ICollection<MaintenanceReportItem> ReportItems { get; set; } = new List<MaintenanceReportItem>();
    public ICollection<AcceptanceDecision> Decisions { get; set; } = new List<AcceptanceDecision>();
}
