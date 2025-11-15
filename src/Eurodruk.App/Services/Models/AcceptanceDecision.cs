namespace Eurodruk.App.Services.Models;

public class AcceptanceDecision
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public WorkshopTicket? Ticket { get; set; }
    public string Approver { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool Accepted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
