using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Data;

public class DatabaseInitializer
{
    private readonly WorkshopDbContext _context;

    public DatabaseInitializer(WorkshopDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.EnsureCreatedAsync(cancellationToken);

        if (await _context.Tickets.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;
        var tickets = new List<WorkshopTicket>
        {
            new()
            {
                Title = "KBA Compacta 818 — sekcja 3 — błąd E42",
                ErrorCode = "E42",
                MachineName = "KBA Compacta 818",
                ProductionLine = "L3",
                Location = "Sekcja 3",
                OperatorName = "J.D.",
                OperatorDescription = "Podczas startu sekcji 3 pojawia się błąd E42. Maszyna nie pobiera wstęgi z odwijaka.",
                ServiceDescription = "Wymieniono czujnik prędkości oraz nasmarowano prowadnice.",
                CreatedAt = now.AddDays(-2),
                Status = TicketStatus.Open,
                Photos =
                {
                    new TicketPhoto { Url = "https://picsum.photos/seed/kba1/1000/700", UploadedBy = "Operator" },
                    new TicketPhoto { Url = "https://picsum.photos/seed/kba2/1000/700", UploadedBy = "Serwis" }
                },
                Actions =
                {
                    new TicketAction { Author = "Operator", Body = "Maszyna zatrzymuje się tuż po starcie.", Type = TicketActionType.OperatorComment },
                    new TicketAction { Author = "M.S.", Body = "Zamówiony nowy czujnik.", Type = TicketActionType.ServiceComment }
                }
            },
            new()
            {
                Title = "Roland Lithoman IV — odwijak A — czujnik taśmy",
                ErrorCode = "E07",
                MachineName = "Roland Lithoman IV",
                ProductionLine = "L2",
                Location = "Odwijak A",
                OperatorName = "A.K.",
                OperatorDescription = "Czujnik taśmy gubi impuls. Pasek zatrzymuje się.",
                ServiceDescription = "Wymiana czujnika oraz korekta prowadzenia przewodu.",
                CreatedAt = now.AddDays(-3),
                Status = TicketStatus.InProgress,
                Photos =
                {
                    new TicketPhoto { Url = "https://picsum.photos/seed/roland1/1000/700", UploadedBy = "Operator" }
                },
                Actions =
                {
                    new TicketAction { Author = "A.K.", Body = "Błąd pojawia się po 20 min biegu.", Type = TicketActionType.OperatorComment }
                }
            },
            new()
            {
                Title = "Kompresor Atlas — wymiana filtra oleju",
                ErrorCode = "",
                MachineName = "Atlas Copco",
                ProductionLine = "DUR",
                Location = "Filtr oleju",
                OperatorName = "System",
                OperatorDescription = "Wzrost temperatury o 15°C. Filtr zatarasowany.",
                ServiceDescription = "Wymieniono filtr, uruchomiono, temperatura w normie.",
                CreatedAt = now.AddDays(-4),
                Status = TicketStatus.Closed,
                ClosedAt = now.AddDays(-1)
            }
        };

        _context.Tickets.AddRange(tickets);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
