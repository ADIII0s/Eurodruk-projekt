namespace Eurodruk.App.Services.Models;

public class TicketAction
{
    public int Id { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public TicketActionType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int TicketId { get; set; }
    public WorkshopTicket? Ticket { get; set; }
}
