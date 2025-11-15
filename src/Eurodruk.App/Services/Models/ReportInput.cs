namespace Eurodruk.App.Services.Models;

public record ReportInput(
    string Title,
    string Author,
    IReadOnlyCollection<int> TicketIds
);
