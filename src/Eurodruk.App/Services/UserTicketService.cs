using Eurodruk.App.Data;
using Eurodruk.App.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Eurodruk.App.Services;

public class UserTicketService : IUserTicketService
{
    private readonly WorkshopDbContext _context;

    public UserTicketService(WorkshopDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<WorkshopTicket>> GetRecentAsync(int take = 10, CancellationToken cancellationToken = default)
    {
        return await _context.Tickets
            .OrderByDescending(t => t.CreatedAt)
            .Take(take)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
