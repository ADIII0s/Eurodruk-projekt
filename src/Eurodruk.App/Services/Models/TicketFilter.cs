namespace Eurodruk.App.Services.Models;

public record TicketFilter(
    TicketStatus? Status,
    string? Machine,
    string? Line,
    DateTime? From,
    DateTime? To,
    string? Text
);
