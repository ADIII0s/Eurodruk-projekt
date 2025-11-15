using Eurodruk.App.Data;
using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Services;

public class ReportService : IReportService
{
    private readonly WorkshopDbContext _context;

    public ReportService(WorkshopDbContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceReport> CreateReportAsync(ReportInput input, CancellationToken cancellationToken = default)
    {
        var tickets = await _context.Tickets
            .Where(t => input.TicketIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        if (tickets.Count == 0)
        {
            throw new InvalidOperationException("Brak zgłoszeń do raportu");
        }

        var report = new MaintenanceReport
        {
            Title = input.Title,
            Author = input.Author,
            CreatedAt = DateTime.UtcNow,
            Items = tickets.Select(t => new MaintenanceReportItem
            {
                TicketId = t.Id,
                Summary = BuildSummary(t)
            }).ToList()
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync(cancellationToken);
        return report;
    }

    public async Task<IReadOnlyList<MaintenanceReport>> GetReportsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Reports
            .Include(r => r.Items)
            .ThenInclude(i => i.Ticket)
            .OrderByDescending(r => r.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    private static string BuildSummary(WorkshopTicket ticket)
    {
        return $"{ticket.Title}\nMaszyna: {ticket.MachineName} ({ticket.ProductionLine})\n" +
               $"Operator: {ticket.OperatorName}\n" +
               $"Opis: {ticket.OperatorDescription}\n" +
               (string.IsNullOrWhiteSpace(ticket.ServiceDescription)
                    ? ""
                    : $"Serwis: {ticket.ServiceDescription}");
    }
}
