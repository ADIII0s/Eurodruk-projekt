namespace Eurodruk.App.Services.Models;

public class TicketPhoto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? Caption { get; set; }
    public string UploadedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int TicketId { get; set; }
    public WorkshopTicket? Ticket { get; set; }
}
