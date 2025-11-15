using Eurodruk.App.Data;
using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Services;

public class AcceptanceService : IAcceptanceService
{
    private readonly WorkshopDbContext _context;

    public AcceptanceService(WorkshopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<WorkshopTicket>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .Include(t => t.Decisions)
            .Where(t => t.Status == TicketStatus.Closed || t.Status == TicketStatus.Rejected)
            .OrderByDescending(t => t.ClosedAt ?? t.CreatedAt)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<AcceptanceDecision> SaveDecisionAsync(int ticketId, bool accepted, string notes, string approver, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);
        if (ticket is null)
        {
            throw new InvalidOperationException("Ticket not found");
        }

        ticket.Status = accepted ? TicketStatus.Accepted : TicketStatus.Rejected;

        var decision = new AcceptanceDecision
        {
            TicketId = ticketId,
            Accepted = accepted,
            Notes = notes,
            Approver = approver,
            CreatedAt = DateTime.UtcNow
        };

        _context.AcceptanceDecisions.Add(decision);
        await _context.SaveChangesAsync(cancellationToken);
        return decision;
    }
}
